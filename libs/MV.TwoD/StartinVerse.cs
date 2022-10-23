﻿using MV.Forms;
using MV.Interfaces;
using MV.Skia;

namespace MV.TwoD
{
    internal class StartingVerse : IVerse2d<Skia2dEnviorment>
    {
        private IMetaVerseRunner? _ctx;

        public Task InitEngine(Skia2dEnviorment env)
        {
            return Task.CompletedTask;
        }

        public Task Start()
        {
            var b = new Button("START");
            b.Clicked += () =>
            {
                Console.WriteLine("a");
            };
            _ctx.Show(b);

            return Task.CompletedTask;
        }

        public Task Init(IMetaVerseRunner? context)
        {
            this._ctx = context;
            return Task.CompletedTask;
        }
    }
}
