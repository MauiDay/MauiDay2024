﻿using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UiReactorMaui.Pages;

namespace UiReactorMaui
{
    internal class AppShell : Component
    {
        public override VisualNode Render()
            => new Shell
            {
            new TabBar()
            {
                new ShellContent("MainPage")
                .RenderContent(() => new MainPage()),
                new ShellContent("OtherPage")
                .RenderContent(() => new OtherPage()),

            },
            new FlyoutItem("MainPage")
            {
                new ShellContent()
                    .Title("MainPage")
                    .RenderContent(()=>new MainPage())
            },
            new FlyoutItem("OtherPage")
            {
                new ShellContent()
                    .Title("OtherPage")
                    .RenderContent(()=>new OtherPage())
            }
            }
            .ItemTemplate(RenderItemTemplate);

        static VisualNode RenderItemTemplate(MauiControls.BaseShellItem item)
            => new Grid("68", "*")
            {
            new Label(item.Title)
                .VCenter()
                .Margin(10,0)
            };
    }
}