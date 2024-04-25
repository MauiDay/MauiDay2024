using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiReactorMaui.Pages.Shared
{
    internal static class Buttons
    {
        public static Button PrimaryButton(string label, Action selectedHandler) =>
            new Button(label)
                .OnClicked(selectedHandler)
                // .FontSize(42)
                .HCenter();

        //public static Button SecondaryButton(string label, Action selectedHandler) =>
        //    new Button(label)
        //        .OnClicked(selectedHandler)
        //        .BackgroundColor(Colors.Grey)
        //        .HCenter();
    }
}
