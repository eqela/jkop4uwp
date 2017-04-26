
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

namespace cave.ui
{
	public class PopupDialogManager
	{
		private cave.GuiApplicationContext context = null;
		private Windows.UI.Xaml.UIElement parent = null;
		private cave.Color backgroundColor = null;
		private cave.Color headerBackgroundColor = null;
		private cave.Color headerTextColor = null;
		private cave.Color messageTextColor = null;
		private cave.Color positiveButtonColor = null;
		private cave.Color negativeButtonColor = null;

		public PopupDialogManager(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement parent) {
			this.context = context;
			this.parent = parent;
			positiveButtonColor = cave.Color.forRGB(128, 204, 128);
			negativeButtonColor = cave.Color.forRGB(204, 128, 128);
			backgroundColor = null;
			headerBackgroundColor = null;
			headerTextColor = null;
			messageTextColor = null;
		}

		public cave.ui.PopupDialogManager setButtonColor(cave.Color color) {
			positiveButtonColor = color;
			negativeButtonColor = color;
			return(this);
		}

		public void showTextInputDialog(string title, string prompt, System.Action<string> callback = null) {
			checkForDefaultColors();
			var mm2 = context.getWidthValue("2mm");
			var mm3 = context.getWidthValue("3mm");
			var widget = new cave.ui.LayerWidget(context);
			widget.setWidgetWidthRequest(context.getWidthValue("100mm"));
			widget.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, backgroundColor));
			var titleLabel = cave.ui.LabelWidget.forText(context, title);
			titleLabel.setWidgetFontSize((double)mm3);
			titleLabel.setWidgetTextColor(headerTextColor);
			titleLabel.setWidgetFontBold(true);
			var box = new cave.ui.VerticalBoxWidget(context);
			box.addWidget((Windows.UI.Xaml.UIElement)new cave.ui.LayerWidget(context).addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, headerBackgroundColor)).addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)titleLabel, (double)0, 0.50).setWidgetMargin(mm3)));
			var sbox = new cave.ui.VerticalBoxWidget(context);
			sbox.setWidgetMargin(mm3);
			sbox.setWidgetSpacing(mm3);
			var messageLabel = cave.ui.LabelWidget.forText(context, prompt);
			messageLabel.setWidgetTextAlign(cave.ui.LabelWidget.ALIGN_CENTER);
			messageLabel.setWidgetFontSize((double)mm3);
			messageLabel.setWidgetTextColor(messageTextColor);
			sbox.addWidget((Windows.UI.Xaml.UIElement)messageLabel);
			var input = new cave.ui.TextInputWidget(context);
			input.setWidgetBackgroundColor(cave.Color.forRGB(200, 200, 200));
			input.setWidgetPadding(context.getHeightValue("2mm"));
			input.setWidgetFontSize((double)context.getHeightValue("3000um"));
			sbox.addWidget((Windows.UI.Xaml.UIElement)input);
			var buttons = new cave.ui.HorizontalBoxWidget(context);
			buttons.setWidgetSpacing(mm3);
			var noButton = cave.ui.TextButtonWidget.forText(context, "Cancel", null);
			noButton.setWidgetBackgroundColor(negativeButtonColor);
			buttons.addWidget((Windows.UI.Xaml.UIElement)noButton, 1.00);
			var yesButton = cave.ui.TextButtonWidget.forText(context, "OK", null);
			yesButton.setWidgetBackgroundColor(positiveButtonColor);
			buttons.addWidget((Windows.UI.Xaml.UIElement)yesButton, 1.00);
			sbox.addWidget((Windows.UI.Xaml.UIElement)buttons);
			box.addWidget((Windows.UI.Xaml.UIElement)sbox);
			widget.addWidget((Windows.UI.Xaml.UIElement)box);
			var pp = cave.ui.PopupWidget.forContentWidget(context, (Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)widget, mm2));
			var cb = callback;
			pp.showPopup(parent);
			yesButton.setWidgetClickHandler(() => {
				pp.hidePopup();
				if(cb != null) {
					cb(input.getWidgetText());
				}
			});
			noButton.setWidgetClickHandler(() => {
				pp.hidePopup();
				if(cb != null) {
					cb(null);
				}
			});
		}

		public void showMessageDialog(string title, string message, System.Action callback = null) {
			checkForDefaultColors();
			var mm2 = context.getWidthValue("2mm");
			var mm3 = context.getWidthValue("3mm");
			var widget = new cave.ui.LayerWidget(context);
			widget.setWidgetWidthRequest(context.getWidthValue("100mm"));
			widget.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, cave.Color.white()));
			var titleLabel = cave.ui.LabelWidget.forText(context, title);
			titleLabel.setWidgetFontSize((double)mm3);
			titleLabel.setWidgetTextColor(headerTextColor);
			titleLabel.setWidgetFontBold(true);
			var box = new cave.ui.VerticalBoxWidget(context);
			box.addWidget((Windows.UI.Xaml.UIElement)new cave.ui.LayerWidget(context).addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, headerBackgroundColor)).addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)titleLabel, (double)0, 0.50).setWidgetMargin(mm3)));
			var sbox = new cave.ui.VerticalBoxWidget(context);
			sbox.setWidgetMargin(mm3);
			sbox.setWidgetSpacing(mm3);
			var messageLabel = cave.ui.LabelWidget.forText(context, message);
			messageLabel.setWidgetTextAlign(cave.ui.LabelWidget.ALIGN_CENTER);
			messageLabel.setWidgetFontSize((double)mm3);
			messageLabel.setWidgetTextColor(messageTextColor);
			sbox.addWidget((Windows.UI.Xaml.UIElement)messageLabel);
			var buttons = new cave.ui.HorizontalBoxWidget(context);
			buttons.setWidgetSpacing(mm3);
			var okButton = cave.ui.TextButtonWidget.forText(context, "OK", null);
			okButton.setWidgetBackgroundColor(positiveButtonColor);
			buttons.addWidget((Windows.UI.Xaml.UIElement)okButton, 1.00);
			sbox.addWidget((Windows.UI.Xaml.UIElement)buttons);
			box.addWidget((Windows.UI.Xaml.UIElement)sbox);
			widget.addWidget((Windows.UI.Xaml.UIElement)box);
			var pp = cave.ui.PopupWidget.forContentWidget(context, (Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)widget, mm2));
			var cb = callback;
			pp.showPopup(parent);
			okButton.setWidgetClickHandler(() => {
				pp.hidePopup();
				if(cb != null) {
					cb();
				}
			});
		}

		public void showConfirmDialog(string title, string message, System.Action okcallback, System.Action cancelcallback) {
			checkForDefaultColors();
			var mm2 = context.getWidthValue("2mm");
			var mm3 = context.getWidthValue("3mm");
			var widget = new cave.ui.LayerWidget(context);
			widget.setWidgetWidthRequest(context.getWidthValue("100mm"));
			widget.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, cave.Color.white()));
			var titleLabel = cave.ui.LabelWidget.forText(context, title);
			titleLabel.setWidgetFontSize((double)mm3);
			titleLabel.setWidgetTextColor(headerTextColor);
			titleLabel.setWidgetFontBold(true);
			var box = new cave.ui.VerticalBoxWidget(context);
			box.addWidget((Windows.UI.Xaml.UIElement)new cave.ui.LayerWidget(context).addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, headerBackgroundColor)).addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)titleLabel, (double)0, 0.50).setWidgetMargin(mm3)));
			var sbox = new cave.ui.VerticalBoxWidget(context);
			sbox.setWidgetMargin(mm3);
			sbox.setWidgetSpacing(mm3);
			var messageLabel = cave.ui.LabelWidget.forText(context, message);
			messageLabel.setWidgetTextAlign(cave.ui.LabelWidget.ALIGN_CENTER);
			messageLabel.setWidgetFontSize((double)mm3);
			messageLabel.setWidgetTextColor(messageTextColor);
			sbox.addWidget((Windows.UI.Xaml.UIElement)messageLabel);
			var buttons = new cave.ui.HorizontalBoxWidget(context);
			buttons.setWidgetSpacing(mm3);
			var noButton = cave.ui.TextButtonWidget.forText(context, "NO", null);
			noButton.setWidgetBackgroundColor(negativeButtonColor);
			buttons.addWidget((Windows.UI.Xaml.UIElement)noButton, 1.00);
			var yesButton = cave.ui.TextButtonWidget.forText(context, "YES", null);
			yesButton.setWidgetBackgroundColor(positiveButtonColor);
			buttons.addWidget((Windows.UI.Xaml.UIElement)yesButton, 1.00);
			sbox.addWidget((Windows.UI.Xaml.UIElement)buttons);
			box.addWidget((Windows.UI.Xaml.UIElement)sbox);
			widget.addWidget((Windows.UI.Xaml.UIElement)box);
			var pp = cave.ui.PopupWidget.forContentWidget(context, (Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)widget, mm2));
			var okcb = okcallback;
			var cancelcb = cancelcallback;
			pp.showPopup(parent);
			yesButton.setWidgetClickHandler(() => {
				pp.hidePopup();
				if(okcb != null) {
					okcb();
				}
			});
			noButton.setWidgetClickHandler(() => {
				pp.hidePopup();
				if(cancelcb != null) {
					cancelcb();
				}
			});
		}

		public void showErrorDialog(string message, System.Action callback = null) {
			showMessageDialog("Error", message, callback);
		}

		public void checkForDefaultColors() {
			var bgc = backgroundColor;
			if(bgc == null) {
				backgroundColor = cave.Color.white();
			}
			var hbg = headerBackgroundColor;
			if(hbg == null) {
				headerBackgroundColor = cave.Color.black();
			}
			var htc = headerTextColor;
			if(htc == null) {
				if(headerBackgroundColor.isDarkColor()) {
					headerTextColor = cave.Color.white();
				}
				else {
					headerTextColor = cave.Color.black();
				}
			}
			var mtc = messageTextColor;
			if(mtc == null) {
				if(backgroundColor.isDarkColor()) {
					messageTextColor = cave.Color.white();
				}
				else {
					messageTextColor = cave.Color.black();
				}
			}
		}

		public cave.GuiApplicationContext getContext() {
			return(context);
		}

		public cave.ui.PopupDialogManager setContext(cave.GuiApplicationContext v) {
			context = v;
			return(this);
		}

		public Windows.UI.Xaml.UIElement getParent() {
			return(parent);
		}

		public cave.ui.PopupDialogManager setParent(Windows.UI.Xaml.UIElement v) {
			parent = v;
			return(this);
		}

		public cave.Color getBackgroundColor() {
			return(backgroundColor);
		}

		public cave.ui.PopupDialogManager setBackgroundColor(cave.Color v) {
			backgroundColor = v;
			return(this);
		}

		public cave.Color getHeaderBackgroundColor() {
			return(headerBackgroundColor);
		}

		public cave.ui.PopupDialogManager setHeaderBackgroundColor(cave.Color v) {
			headerBackgroundColor = v;
			return(this);
		}

		public cave.Color getHeaderTextColor() {
			return(headerTextColor);
		}

		public cave.ui.PopupDialogManager setHeaderTextColor(cave.Color v) {
			headerTextColor = v;
			return(this);
		}

		public cave.Color getMessageTextColor() {
			return(messageTextColor);
		}

		public cave.ui.PopupDialogManager setMessageTextColor(cave.Color v) {
			messageTextColor = v;
			return(this);
		}

		public cave.Color getPositiveButtonColor() {
			return(positiveButtonColor);
		}

		public cave.ui.PopupDialogManager setPositiveButtonColor(cave.Color v) {
			positiveButtonColor = v;
			return(this);
		}

		public cave.Color getNegativeButtonColor() {
			return(negativeButtonColor);
		}

		public cave.ui.PopupDialogManager setNegativeButtonColor(cave.Color v) {
			negativeButtonColor = v;
			return(this);
		}
	}
}
