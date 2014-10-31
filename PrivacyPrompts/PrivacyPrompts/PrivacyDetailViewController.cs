using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace PrivacyPrompts {

	public class PrivacyDetailViewController : UIViewController	{

		protected Func<String> CheckAccess;
		protected Action RequestAccess;

		protected UILabel titleLabel;
		protected UILabel accessStatus;
		protected UIButton requestAccessButton;

		public PrivacyDetailViewController (Func<String> checkAccess, Action requestAccess) 
		{
			this.CheckAccess = checkAccess;
			this.RequestAccess = requestAccess;
		}

		public void AddBaseElements(UIView mainView)
		{
			titleLabel = new UILabel (CGRect.Empty);
			titleLabel.TextAlignment = UITextAlignment.Center;

			accessStatus = new UILabel (CGRect.Empty);
			accessStatus.TextAlignment = UITextAlignment.Center;

			requestAccessButton = UIButton.FromType (UIButtonType.RoundedRect);
			requestAccessButton.TouchUpInside += (s, e) => RequestAccess ();

			titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			accessStatus.TranslatesAutoresizingMaskIntoConstraints = false;
			requestAccessButton.TranslatesAutoresizingMaskIntoConstraints = false;


			// View-level constraints to set constant size values
			titleLabel.AddConstraints (new [] {
				NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 14),
				NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 180),
			});

			accessStatus.AddConstraints (new[] {
				NSLayoutConstraint.Create (accessStatus, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 18),
				NSLayoutConstraint.Create (accessStatus, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 180),
			});

			requestAccessButton.AddConstraints (new[] {
				NSLayoutConstraint.Create (requestAccessButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 14),
				NSLayoutConstraint.Create (requestAccessButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 180),
			});
			mainView.AddSubview (titleLabel);
			mainView.AddSubview (accessStatus);
			mainView.AddSubview (requestAccessButton);

			// Container view-level constraints to set the position of each subview
			mainView.AddConstraints (new[] {
				NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create (titleLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, mainView, NSLayoutAttribute.Top, 1, 80),

				NSLayoutConstraint.Create (accessStatus, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, titleLabel, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create (accessStatus, NSLayoutAttribute.Top, NSLayoutRelation.Equal, titleLabel, NSLayoutAttribute.Bottom, 1, 40),
				NSLayoutConstraint.Create (requestAccessButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, titleLabel, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create (requestAccessButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, accessStatus, NSLayoutAttribute.Bottom, 1, 40),
			});
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AddBaseElements (this.View);
			this.View.BackgroundColor = UIColor.White;

			titleLabel.Text = Title;
			titleLabel.Text = "Title";
			accessStatus.Text = "Indeterminate";
			requestAccessButton.SetTitle ("Request access", UIControlState.Normal);

			accessStatus.Text = CheckAccess ();
		}
	}
}