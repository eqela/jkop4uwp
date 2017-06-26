
/*
 * This file is part of Jkop for UWP
 * Copyright (c) 2016-2017 Job and Esther Technologies, Inc.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace capex.data {
	public abstract class SQLDatabase
	{
		public SQLDatabase() {
		}

		private cape.LoggingContext logger = null;
		public abstract capex.data.SQLStatement prepare(string sql);
		public abstract string getDatabaseTypeId();
		public abstract capex.data.SQLStatement prepareCreateTableStatement(string table, System.Collections.Generic.List<capex.data.SQLTableColumnInfo> columns);
		public abstract capex.data.SQLStatement prepareDeleteTableStatement(string table);
		public abstract capex.data.SQLStatement prepareCreateIndexStatement(string table, string column, bool unique);
		public abstract long getLastInsertId(string table);
		public abstract void close(System.Action callback);
		public abstract void execute(capex.data.SQLStatement stmt, System.Action<bool> callback);
		public abstract void query(capex.data.SQLStatement stmt, System.Action<capex.data.SQLResultSetIterator> callback);
		public abstract void querySingleRow(capex.data.SQLStatement stmt, System.Action<cape.DynamicMap> callback);
		public abstract void tableExists(string table, System.Action<bool> callback);
		public abstract void queryAllTableNames(System.Action<System.Collections.Generic.List<object>> callback);
		public abstract void close();
		public abstract bool execute(capex.data.SQLStatement stmt);
		public abstract capex.data.SQLResultSetIterator query(capex.data.SQLStatement stmt);
		public abstract cape.DynamicMap querySingleRow(capex.data.SQLStatement stmt);
		public abstract bool tableExists(string table);
		public abstract System.Collections.Generic.List<object> queryAllTableNames();

		public virtual bool ensureTableExists(capex.data.SQLTableInfo table) {
			if(!(table != null)) {
				return(false);
			}
			var name = table.getName();
			if(cape.String.isEmpty(name)) {
				return(false);
			}
			if(tableExists(name)) {
				return(true);
			}
			if(!execute(prepareCreateTableStatement(name, table.getColumns()))) {
				return(false);
			}
			var array = table.getIndexes();
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var cii = array[n];
					if(cii != null) {
						if(execute(prepareCreateIndexStatement(name, cii.getColumn(), cii.getUnique())) == false) {
							execute(prepareDeleteTableStatement(name));
						}
					}
				}
			}
			return(true);
		}

		public virtual void ensureTableExists(capex.data.SQLTableInfo table, System.Action<bool> callback) {
			var v = ensureTableExists(table);
			if(callback != null) {
				callback(v);
			}
		}

		private string createColumnSelectionString(object[] columns) {
			if(columns == null || columns.Length < 1) {
				return("*");
			}
			var sb = new cape.StringBuilder();
			var first = true;
			if(columns != null) {
				var n = 0;
				var m = columns.Length;
				for(n = 0 ; n < m ; n++) {
					var column = columns[n] as string;
					if(column != null) {
						if(first == false) {
							sb.append(", ");
						}
						sb.append(column);
						first = false;
					}
				}
			}
			return(sb.toString());
		}

		private string createOrderByString(object[] order) {
			if(order == null || order.Length < 1) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append(" ORDER BY ");
			var first = true;
			if(order != null) {
				var n = 0;
				var m = order.Length;
				for(n = 0 ; n < m ; n++) {
					var rule = order[n];
					if(rule != null) {
						if(first == false) {
							sb.append(", ");
						}
						if(rule is capex.data.SQLOrderingRule) {
							var sr = (capex.data.SQLOrderingRule)rule;
							sb.append(sr.getColumn());
							sb.append(' ');
							if(sr.getDescending()) {
								sb.append("DESC");
							}
							else {
								sb.append("ASC");
							}
						}
						else {
							var str = cape.String.asString(rule);
							if(!(str != null)) {
								str = "unknown";
							}
							sb.append(str);
							sb.append(" DESC");
						}
						first = false;
					}
				}
			}
			return(sb.toString());
		}

		public capex.data.SQLStatement prepareQueryAllStatement(string table) {
			return(prepareQueryAllStatement(table, null, null));
		}

		public capex.data.SQLStatement prepareQueryAllStatement(string table, object[] columns) {
			return(prepareQueryAllStatement(table, columns, null));
		}

		public virtual capex.data.SQLStatement prepareQueryAllStatement(string table, object[] columns, object[] order) {
			var sb = new cape.StringBuilder();
			sb.append("SELECT ");
			sb.append(createColumnSelectionString(columns));
			sb.append(" FROM ");
			sb.append(table);
			sb.append(createOrderByString(order));
			sb.append(";");
			return(prepare(sb.toString()));
		}

		public virtual capex.data.SQLStatement prepareCountRecordsStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria) {
			var sb = new cape.StringBuilder();
			sb.append("SELECT COUNT(*) AS count FROM ");
			sb.append(table);
			var first = true;
			System.Collections.Generic.List<object> keys = null;
			if(criteria != null) {
				keys = cape.Map.getKeys(criteria) as System.Collections.Generic.List<object>;
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n] as string;
						if(key != null) {
							if(first) {
								sb.append(" WHERE ");
								first = false;
							}
							else {
								sb.append(" AND ");
							}
							sb.append(key);
							sb.append(" = ?");
						}
					}
				}
			}
			sb.append(';');
			var sql = sb.toString();
			var stmt = prepare(sql);
			if(stmt == null) {
				return(null);
			}
			if(keys != null) {
				if(keys != null) {
					var n2 = 0;
					var m2 = keys.Count;
					for(n2 = 0 ; n2 < m2 ; n2++) {
						var key1 = keys[n2] as string;
						if(key1 != null) {
							var val = cape.String.asString(cape.Map.get(criteria, key1));
							stmt.addParamString(val);
						}
					}
				}
			}
			return(stmt);
		}

		public virtual capex.data.SQLStatement prepareQueryWithCriteriaStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria) {
			return(prepareQueryWithCriteriaStatement(table, criteria, 0, 0, null, null));
		}

		public virtual capex.data.SQLStatement prepareQueryWithCriteriaStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria, int limit) {
			return(prepareQueryWithCriteriaStatement(table, criteria, limit, 0, null, null));
		}

		public virtual capex.data.SQLStatement prepareQueryWithCriteriaStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria, int limit, int offset) {
			return(prepareQueryWithCriteriaStatement(table, criteria, limit, offset, null, null));
		}

		public virtual capex.data.SQLStatement prepareQueryWithCriteriaStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria, int limit, int offset, object[] columns) {
			return(prepareQueryWithCriteriaStatement(table, criteria, limit, offset, columns, null));
		}

		public virtual capex.data.SQLStatement prepareQueryWithCriteriaStatement(string table, System.Collections.Generic.Dictionary<object,object> criteria, int limit, int offset, object[] columns, object[] order) {
			var sb = new cape.StringBuilder();
			sb.append("SELECT ");
			sb.append(createColumnSelectionString(columns));
			sb.append(" FROM ");
			sb.append(table);
			var first = true;
			System.Collections.Generic.List<object> keys = null;
			if(criteria != null) {
				keys = cape.Map.getKeys(criteria) as System.Collections.Generic.List<object>;
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n] as string;
						if(key != null) {
							if(first) {
								sb.append(" WHERE ");
								first = false;
							}
							else {
								sb.append(" AND ");
							}
							sb.append(key);
							sb.append(" = ?");
						}
					}
				}
			}
			sb.append(createOrderByString(order));
			if(limit > 0) {
				sb.append(" LIMIT ");
				sb.append(cape.String.forInteger(limit));
			}
			if(offset > 0) {
				sb.append(" OFFSET ");
				sb.append(cape.String.forInteger(offset));
			}
			sb.append(';');
			var sql = sb.toString();
			var stmt = prepare(sql);
			if(stmt == null) {
				return(null);
			}
			if(keys != null) {
				if(keys != null) {
					var n2 = 0;
					var m2 = keys.Count;
					for(n2 = 0 ; n2 < m2 ; n2++) {
						var key1 = keys[n2] as string;
						if(key1 != null) {
							var val = cape.String.asString(cape.Map.get(criteria, key1));
							stmt.addParamString(val);
						}
					}
				}
			}
			return(stmt);
		}

		public virtual capex.data.SQLStatement prepareQueryDistinctValuesStatement(string table, string column) {
			if(cape.String.isEmpty(table) || cape.String.isEmpty(column)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append("SELECT DISTINCT ");
			sb.append(column);
			sb.append(" FROM ");
			sb.append(table);
			sb.append(";");
			return(prepare(sb.toString()));
		}

		public virtual capex.data.SQLStatement prepareInsertStatement(string table, cape.DynamicMap data) {
			if(cape.String.isEmpty(table) || data == null || data.getCount() < 1) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append("INSERT INTO ");
			sb.append(table);
			sb.append(" ( ");
			var first = true;
			var keys = data.getKeys();
			if(keys != null) {
				var n = 0;
				var m = keys.Count;
				for(n = 0 ; n < m ; n++) {
					var key = keys[n];
					if(key != null) {
						if(first == false) {
							sb.append(',');
						}
						sb.append(key as string);
						first = false;
					}
				}
			}
			sb.append(" ) VALUES ( ");
			first = true;
			if(keys != null) {
				var n2 = 0;
				var m2 = keys.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var key1 = keys[n2];
					if(key1 != null) {
						if(first == false) {
							sb.append(',');
						}
						sb.append('?');
						first = false;
					}
				}
			}
			sb.append(" );");
			var stmt = prepare(sb.toString());
			if(stmt == null) {
				return(null);
			}
			if(keys != null) {
				var n3 = 0;
				var m3 = keys.Count;
				for(n3 = 0 ; n3 < m3 ; n3++) {
					var key2 = keys[n3];
					if(key2 != null) {
						var o = data.get(key2);
						if(o is string || o is cape.StringObject) {
							stmt.addParamString(cape.String.asString(o));
						}
						else if(o is cape.IntegerObject) {
							stmt.addParamInteger(cape.Integer.asInteger(o));
						}
						else if(o is cape.LongIntegerObject) {
							stmt.addParamLongInteger(cape.LongInteger.asLong(o));
						}
						else if(o is cape.DoubleObject) {
							stmt.addParamDouble(cape.Double.asDouble(o));
						}
						else if(o is cape.BufferObject) {
							stmt.addParamBlob(((cape.BufferObject)o).toBuffer());
						}
						else if(o is byte[]) {
							stmt.addParamBlob(o as byte[]);
						}
						else {
							var s = o as string;
							if(object.Equals(s, null)) {
								s = "";
							}
							stmt.addParamString(s);
						}
					}
				}
			}
			return(stmt);
		}

		public virtual capex.data.SQLStatement prepareUpdateStatement(string table, cape.DynamicMap criteria, cape.DynamicMap data) {
			if(cape.String.isEmpty(table) || data == null || data.getCount() < 1) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append("UPDATE ");
			sb.append(table);
			sb.append(" SET ");
			var @params = new System.Collections.Generic.List<object>();
			var first = true;
			var keys = data.iterateKeys();
			while(keys != null) {
				var key = keys.next();
				if(object.Equals(key, null)) {
					break;
				}
				if(first == false) {
					sb.append(", ");
				}
				sb.append(key);
				sb.append(" = ?");
				first = false;
				@params.Add(data.get(key));
			}
			if(criteria != null && criteria.getCount() > 0) {
				sb.append(" WHERE ");
				first = true;
				var criterias = criteria.iterateKeys();
				while(criterias != null) {
					var criterium = criterias.next();
					if(object.Equals(criterium, null)) {
						break;
					}
					if(first == false) {
						sb.append(" AND ");
					}
					sb.append(criterium);
					sb.append(" = ?");
					first = false;
					@params.Add(criteria.get(criterium));
				}
			}
			sb.append(';');
			var stmt = prepare(sb.toString());
			if(stmt == null) {
				return(null);
			}
			if(@params != null) {
				var n = 0;
				var m = @params.Count;
				for(n = 0 ; n < m ; n++) {
					var o = @params[n];
					if(o != null) {
						if(o is byte[]) {
							stmt.addParamBlob(cape.Buffer.asBuffer(o));
						}
						else {
							var s = cape.String.asString(o);
							if(object.Equals(s, null)) {
								s = "";
							}
							stmt.addParamString(s);
						}
					}
				}
			}
			return(stmt);
		}

		public virtual capex.data.SQLStatement prepareDeleteStatement(string table, cape.DynamicMap criteria) {
			if(cape.String.isEmpty(table)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append("DELETE FROM ");
			sb.append(table);
			var @params = new System.Collections.Generic.List<object>();
			if(criteria != null && criteria.getCount() > 0) {
				sb.append(" WHERE ");
				var first = true;
				var criterias = criteria.iterateKeys();
				while(criterias != null) {
					var criterium = criterias.next();
					if(object.Equals(criterium, null)) {
						break;
					}
					if(first == false) {
						sb.append(" AND ");
					}
					sb.append(criterium);
					sb.append(" = ?");
					first = false;
					@params.Add(criteria.get(criterium));
				}
			}
			sb.append(';');
			var stmt = prepare(sb.toString());
			if(stmt == null) {
				return(null);
			}
			if(@params != null) {
				var n = 0;
				var m = @params.Count;
				for(n = 0 ; n < m ; n++) {
					var o = @params[n];
					if(o != null) {
						if(o is byte[]) {
							stmt.addParamBlob(cape.Buffer.asBuffer(o));
						}
						else {
							var s = cape.String.asString(o);
							if(object.Equals(s, null)) {
								s = "";
							}
							stmt.addParamString(s);
						}
					}
				}
			}
			return(stmt);
		}

		public cape.LoggingContext getLogger() {
			return(logger);
		}

		public capex.data.SQLDatabase setLogger(cape.LoggingContext v) {
			logger = v;
			return(this);
		}
	}
}
