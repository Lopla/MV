using System;
using System.Threading.Tasks;
using MV.Interfaces;
using MV.Loader;
using MV.Models;

namespace MV.Client
{
    // ReSharper disable once InconsistentNaming

    public class MVClient
    {
        private readonly DownLoader _downLoader;
        private readonly IMetaVerseRunner _metaVerse;
        private readonly IManifest _startingMetaVerse;
        private bool _isInitialized;

        public MVClient(
            IMetaVerseRunner metaVerse,
            IAssemblyContext context = null,
            bool useFilesInsteadOfStream = false,
            IManifest startingMetaVerse = null)
        {
            _metaVerse = metaVerse;
            _startingMetaVerse = startingMetaVerse;

            var assemblyContext = context ?? new SeparatedDomainContext();
            _downLoader = new DownLoader(assemblyContext, useFilesInsteadOfStream);
        }

        public async Task Start()
        {
            //// endless loop:
            await _metaVerse.Start();
        }

        public async Task Init()
        {
            await _metaVerse.Init();

            _isInitialized = true;
            await LoadDefaultMetaVerse();
        }

        public async Task DownloadMetaVerse(VerseReference reference)
        {
            var downloadedVerseDefinition = await _downLoader.DownloadDefinition(reference.GH);

            await InitializeMetaVerse(downloadedVerseDefinition);
        }

        public async Task LoadDefaultMetaVerse()
        {
            if (this._startingMetaVerse != null)
            {
                await InitializeMetaVerse(this._startingMetaVerse);
            }
            else
            {
                await DownloadMetaVerse(new VerseReference
                {
                    N = '0',
                    GH = "llaagg/mv-home/releases/download/v0.92.7/Home.dll",
                    Name = new I18NString("Home")
                });
            }
        }

        public async Task InitializeMetaVerse(IManifest manifest)
        {
            if (!_isInitialized)
                throw new NotSupportedException("Loading metaverses before init finalized is not available.");

            var verse = manifest.Verse();
            await _metaVerse.InitVerse(verse);
        }
    }
}