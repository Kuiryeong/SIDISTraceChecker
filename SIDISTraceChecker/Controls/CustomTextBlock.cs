using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SIDISTraceChecker.Controls
{
    internal class CustomTextBlock : Control
    {
        private const string TEXT_DISPLAY_PART_NAME = "PART_TextDisplay";

        private TextBlock _displayTextBlock;

        public static readonly DependencyProperty HighlightTextProperty =
            DependencyProperty.Register("HighlightText", typeof(string), typeof(CustomTextBlock),
                new PropertyMetadata(string.Empty, OnHighlightTextPropertyChanged));

        public string HighlightText
        {
            get { return (string)GetValue(HighlightTextProperty); }
            set { SetValue(HighlightTextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            TextBlock.TextProperty.AddOwner(typeof(CustomTextBlock),
                new PropertyMetadata(string.Empty, OnHighlightTextPropertyChanged));

        public string Text
        {
            get => string.IsNullOrEmpty((string)GetValue(TextProperty)) ? "" : (string)GetValue(TextProperty);
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty HighlightRunStyleProperty =
            DependencyProperty.Register("HighlightRunStyle", typeof(Style), typeof(CustomTextBlock),
                new PropertyMetadata(CreateDefaultHighlightRunStyle()));

        private static object CreateDefaultHighlightRunStyle()
        {
            Style style = new Style(typeof(Run));
            style.Setters.Add(new Setter(Run.BackgroundProperty, Brushes.Yellow));

            return style;
        }

        public Style HighlightRunStyle
        {
            get { return (Style)GetValue(HighlightRunStyleProperty); }
            set { SetValue(HighlightRunStyleProperty, value); }
        }

        static CustomTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBlock), new FrameworkPropertyMetadata(typeof(CustomTextBlock)));
        }

        public override void OnApplyTemplate()
        {
            _displayTextBlock = Template.FindName(TEXT_DISPLAY_PART_NAME, this) as TextBlock;
            UpdateHighlightDisplay();

            base.OnApplyTemplate();
        }

        private static void OnHighlightTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextBlock highlightTextBlock)
            {
                highlightTextBlock.UpdateHighlightDisplay();
            }
        }

        private void UpdateHighlightDisplay()
        {
            if (_displayTextBlock != null)
            {
                _displayTextBlock.Inlines.Clear();

                if (HighlightText is null && Text is null) return;

                int highlightTextLength = HighlightText is null ? 0 : HighlightText.Length;
                if (highlightTextLength == 0)
                {
                    _displayTextBlock.Text = Text;
                }
                else
                {
                    for (int i = 0; i < Text.Length; i++)
                    {
                        if (i + highlightTextLength > Text.Length)
                        {
                            _displayTextBlock.Inlines.Add(new Run(Text.Substring(i)));
                            break;
                        }

                        int nextHighlightTextIndex = Text.IndexOf(HighlightText, i, System.StringComparison.OrdinalIgnoreCase);
                        if (nextHighlightTextIndex == -1)
                        {
                            _displayTextBlock.Inlines.Add(new Run(Text.Substring(i)));
                            break;
                        }

                        _displayTextBlock.Inlines.Add(new Run(Text.Substring(i, nextHighlightTextIndex - i)));
                        _displayTextBlock.Inlines.Add(CreateHighlightedRun(Text.Substring(nextHighlightTextIndex, HighlightText.Length)));

                        i = nextHighlightTextIndex + highlightTextLength - 1;
                    }
                }
            }
        }

        private Run CreateHighlightedRun(string text)
        {
            return new Run(text)
            {
                Style = HighlightRunStyle
            };
        }
    }
}
