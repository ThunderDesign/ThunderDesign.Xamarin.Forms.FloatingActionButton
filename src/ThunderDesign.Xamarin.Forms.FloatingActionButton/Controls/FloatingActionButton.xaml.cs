using Sharpnado.Shades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ThunderDesign.Net.Threading.HelperClasses;
using ThunderDesign.Xamarin.Forms.FloatingActionButton.Extentions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Button;

namespace ThunderDesign.Xamarin.Forms.FloatingActionButton.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionButton : ContentView, IButtonController
    {
        #region constructors
        public FloatingActionButton() : base()
        {
            base.BackgroundColor = Color.Transparent;
            InitializeComponent();
            _Shades = new List<Shade>
            {
                new Shade() { Color = _DefaultShadowColor, Offset = _DefaultShadowOffset, Opacity = _DefaultShadowOpacity, BlurRadius = _DefaultShadowBlurRadius }
            };
            ShadowsFAB.Shades = _Shades;
            //ButtonFAB.
        }
        #endregion

        #region event handlers
        public event EventHandler Clicked;
        public event EventHandler Pressed;
        public event EventHandler Released;
        #endregion

        #region properties
        public static readonly BindableProperty ShadowColorProperty =
            BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(FloatingActionButton), _DefaultShadowColor, propertyChanged: OnShadowColorPropertyChanged);

        public static readonly BindableProperty ShadowOffsetProperty =
            BindableProperty.Create(nameof(ShadowOffset), typeof(Point), typeof(FloatingActionButton), _DefaultShadowOffset, propertyChanged: OnShadowOffsetPropertyChanged);

        public static readonly BindableProperty ShadowOpacityProperty =
            BindableProperty.Create(nameof(ShadowOpacity), typeof(double), typeof(FloatingActionButton), _DefaultShadowOpacity, propertyChanged: OnShadowOpacityPropertyChanged);

        public static readonly BindableProperty ShadowBlurRadiusProperty =
            BindableProperty.Create(nameof(ShadowBlurRadius), typeof(double), typeof(FloatingActionButton), _DefaultShadowBlurRadius, propertyChanged: OnShadowBlurRadiusPropertyChanged);

        public static readonly BindableProperty AnimationEnabledProperty =
            BindableProperty.Create(nameof(AnimationEnabled), typeof(bool), typeof(FloatingActionButton), true);

        public static readonly BindableProperty AnimationShowDelayProperty =
            BindableProperty.Create(nameof(AnimationShowDelay), typeof(int), typeof(FloatingActionButton), 300);

        public static readonly BindableProperty AnimationShowDurationProperty =
            BindableProperty.Create(nameof(AnimationShowDuration), typeof(uint), typeof(FloatingActionButton), 250u);

        public static readonly BindableProperty AnimationHideDurationProperty =
            BindableProperty.Create(nameof(AnimationHideDuration), typeof(uint), typeof(FloatingActionButton), 250u);

        public static readonly BindableProperty AnimationShowEasingProperty =
            BindableProperty.Create(nameof(AnimationShowEasing), typeof(Easing), typeof(FloatingActionButton), Easing.SpringOut);

        public static readonly BindableProperty AnimationHideEasingProperty =
            BindableProperty.Create(nameof(AnimationHideEasing), typeof(Easing), typeof(FloatingActionButton), Easing.SpringIn);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FloatingActionButton));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FloatingActionButton));

        public static readonly BindableProperty ContentLayoutProperty =
            BindableProperty.Create(nameof(ContentLayout), typeof(ButtonContentLayout), typeof(FloatingActionButton), new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, DefaultSpacing));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(FloatingActionButton));

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(FloatingActionButton), Color.Default);

        public static readonly BindableProperty CharacterSpacingProperty =
            BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(FloatingActionButton), 0.0d);

        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(nameof(Font), typeof(Font), typeof(FloatingActionButton), default(Font));

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(FloatingActionButton), default(string));

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(FloatingActionButton), -1.0);

        public static readonly BindableProperty TextTransformProperty =
            BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(FloatingActionButton), TextTransform.Default);

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(FloatingActionButton), FontAttributes.None);

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(FloatingActionButton), -1d);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FloatingActionButton), Color.Default);

        [Obsolete("BorderRadiusProperty is obsolete as of 2.5.0. Please use CornerRadius instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(FloatingActionButton), defaultValue: _DefaultBorderRadius);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FloatingActionButton), defaultValue: _DefaultCornerRadius);

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(FloatingActionButton), default(ImageSource));

        [Obsolete("ImageProperty is obsolete as of 4.0.0. Please use ImageSourceProperty instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(ImageSource), typeof(FloatingActionButton), default(ImageSource));

        public static readonly BindableProperty IsPressedProperty =
            BindableProperty.Create(nameof(IsPressed), typeof(bool), typeof(FloatingActionButton), default(bool));

        public new static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FloatingActionButton), Color.Default);

        public new static readonly BindableProperty WidthRequestProperty =
            BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(FloatingActionButton), -1d);

        public new static readonly BindableProperty HeightRequestProperty =
            BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(FloatingActionButton), -1d);

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        public Point ShadowOffset
        {
            get { return (Point)GetValue(ShadowOffsetProperty); }
            set { SetValue(ShadowOffsetProperty, value); }
        }

        public double ShadowOpacity
        {
            get { return (double)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }

        public double ShadowBlurRadius
        {
            get { return (double)GetValue(ShadowBlurRadiusProperty); }
            set { SetValue(ShadowBlurRadiusProperty, value); }
        }

        public bool AnimationEnabled
        {
            get { return (bool)GetValue(AnimationEnabledProperty); }
            set { SetValue(AnimationEnabledProperty, value); }
        }
        public int AnimationShowDelay
        {
            get { return (int)GetValue(AnimationShowDelayProperty); }
            set { SetValue(AnimationShowDelayProperty, value); }
        }

        public uint AnimationShowDuration
        {
            get { return (uint)GetValue(AnimationShowDurationProperty); }
            set { SetValue(AnimationShowDurationProperty, value); }
        }

        public uint AnimationHideDuration
        {
            get { return (uint)GetValue(AnimationHideDurationProperty); }
            set { SetValue(AnimationHideDurationProperty, value); }
        }

        public Easing AnimationShowEasing
        {
            get { return (Easing)GetValue(AnimationShowEasingProperty); }
            set { SetValue(AnimationShowEasingProperty, value); }
        }

        public Easing AnimationHideEasing
        {
            get { return (Easing)GetValue(AnimationHideEasingProperty); }
            set { SetValue(AnimationHideEasingProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ButtonContentLayout ContentLayout
        {
            get { return (ButtonContentLayout)GetValue(ContentLayoutProperty); }
            set { SetValue(ContentLayoutProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public double CharacterSpacing
        {
            get { return (double)GetValue(CharacterSpacingProperty); }
            set { SetValue(CharacterSpacingProperty, value); }
        }

        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public TextTransform TextTransform
        {
            get { return (TextTransform)GetValue(TextTransformProperty); }
            set { SetValue(TextTransformProperty, value); }
        }

        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        [Obsolete("BorderRadius is obsolete as of 2.5.0. Please use CornerRadius instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        [Obsolete("BorderRadius is obsolete as of 2.5.0. Please use CornerRadius instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            set { SetValue(IsPressedProperty, value); }
        }

        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public new double WidthRequest
        {
            get { return (double)GetValue(WidthRequestProperty); }
            set { SetValue(WidthRequestProperty, value); }
        }

        public new double HeightRequest
        {
            get { return (double)GetValue(HeightRequestProperty); }
            set { SetValue(HeightRequestProperty, value); }
        }
        #endregion

        #region methods
        protected override void OnParentSet()
        {
            base.OnParentSet();

            ThreadHelper.RunAndForget(async () =>
            {
                var page = await this.GetParentAsync<Page>();

                await DelayShowButtonAsync();
                await ShowButtonAsync();
                page.Appearing += Page_Appearing;
                page.Disappearing += Page_Disappearing;
            });
        }

        private void Page_Appearing(object sender, EventArgs e)
        {
            ThreadHelper.RunAndForget(async () => await ShowPageAsync().ConfigureAwait(false));
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            ThreadHelper.RunAndForget(async () => await HidePageAsync().ConfigureAwait(false));
        }

        private async Task ShowPageAsync()
        {
            await DelayShowButtonAsync();
            await ShowButtonAsync();
        }

        private async Task HidePageAsync()
        {
            await HideButtonAsync();
        }

        private async Task ShowButtonAsync()
        {
            if (AnimationEnabled)
                await ShadowsFAB.ScaleTo(1, AnimationShowDuration, easing: AnimationShowEasing);
            else
                await ShadowsFAB.ScaleTo(1);
        }

        private async Task HideButtonAsync()
        {
            if (AnimationEnabled)
                await ShadowsFAB.ScaleTo(0, AnimationHideDuration, easing: AnimationHideEasing);
        }

        private async Task DelayShowButtonAsync()
        {
            if (AnimationEnabled)
                await Task.Delay(AnimationShowDelay >= 0 ? AnimationShowDelay : 0);
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            Released?.Invoke(this, EventArgs.Empty);
        }

        public void SendClicked()
        {
            ButtonFAB.SendClicked();
        }

        public void SendPressed()
        {
            ButtonFAB.SendPressed();
        }

        public void SendReleased()
        {
            ButtonFAB.SendReleased();
        }

        private static void OnShadowColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || newValue == null)
                return;

            if (!(oldValue is Color oldColor) || (!(newValue is Color newColor)))
                return;

            if (oldColor == newColor)
                return;

            var _control = bindable as FloatingActionButton;

            if (_control._Shades.Count == 0)
                return;

            _control._Shades[0].Color = newColor;
        }

        private static void OnShadowOffsetPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || newValue == null)
                return;

            if (!(oldValue is Point oldOffset) || (!(newValue is Point newOffset)))
                return;

            if (oldOffset == newOffset)
                return;

            var _control = bindable as FloatingActionButton;

            if (_control._Shades.Count == 0)
                return;

            _control._Shades[0].Offset = newOffset;
        }

        private static void OnShadowOpacityPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || newValue == null)
                return;

            if (!(oldValue is double oldOpacity) || (!(newValue is double newOpacity)))
                return;

            if (oldOpacity == newOpacity)
                return;

            var _control = bindable as FloatingActionButton;

            if (_control._Shades.Count == 0)
                return;

            _control._Shades[0].Opacity = newOpacity;
        }

        private static void OnShadowBlurRadiusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || newValue == null)
                return;

            if (!(oldValue is double oldBlurRadius) || (!(newValue is double newBlurRadius)))
                return;

            if (oldBlurRadius == newBlurRadius)
                return;

            var _control = bindable as FloatingActionButton;

            if (_control._Shades.Count == 0)
                return;

            _control._Shades[0].BlurRadius = newBlurRadius;
        }
        #endregion

        #region variables
        private const int _DefaultBorderRadius = 5;
        private const int _DefaultCornerRadius = -1;
        private static readonly Color _DefaultShadowColor = Color.Black;
        private static readonly Point _DefaultShadowOffset = new Point(0, 10);
        private const double _DefaultShadowOpacity = 0.5;
        private const double _DefaultShadowBlurRadius = 12f;
        private const double DefaultSpacing = 10;
        private readonly List<Shade> _Shades;
        #endregion
    }
}