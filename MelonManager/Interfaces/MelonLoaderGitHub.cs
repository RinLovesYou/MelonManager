using MelonManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MelonLoader.Interfaces
{
    internal static class MelonLoaderGitHub
    {
        private static readonly HttpClient Client = new HttpClient();

        static MelonLoaderGitHub()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", BuildInfo.Name + " (https://melonwiki.xyz)");
        }
        
        internal static List<ReleaseData> releasesTbl = new List<ReleaseData>();
        internal static ReleaseData LatestVersion => releasesTbl == null ? null : releasesTbl.FirstOrDefault();
        private static string API_URL = Constants.MelonLoaderGitApi;

        internal static void Refresh()
        {
            releasesTbl.Clear();
            
            List<GithubApiRelease> githubApiReleases = Client.GetFromJsonAsync<List<GithubApiRelease>>(API_URL).Result;
            githubApiReleases ??= new List<GithubApiRelease>();
            
            foreach (GithubApiRelease release in githubApiReleases)
            {
                List<GithubApiAsset> githubApiAssets = release.Assets;
                string releaseVersion = release.TagName;
                
                bool hasWinX86 = !releaseVersion.StartsWith("v0.2") && !releaseVersion.StartsWith("v0.1");
                
                ReleaseData releaseData = new ReleaseData()
                {
                    Version = releaseVersion,
                    IsPreRelease = release.Prerelease,
                };

                releaseData.Windows_x64 = release.FindAssetWithFilename("MelonLoader.x64.zip").AsAssetData();
                if (hasWinX86)
                    releaseData.Windows_x86 = release.FindAssetWithFilename("MelonLoader.x86.zip").AsAssetData();

                releasesTbl.Add(releaseData);
            }
            
            releasesTbl = releasesTbl.OrderBy(x => x.Version).ToList();
            releasesTbl.Reverse();
        }

        internal class ReleaseData
        {
            internal string Version;
            internal bool IsPreRelease;

            internal class AssetData
            {
                internal string Download;
                internal string SHA512;
            }
            internal AssetData Windows_x86;
            internal AssetData Windows_x64;
            //internal AssetData Android_Quest;
        }

        private class GithubApiRelease
        {
            [JsonPropertyName("tag_name")]
            public string TagName { get; set; }
            public bool Prerelease { get; set; }

            public List<GithubApiAsset> Assets { get; set; }

            internal GithubApiAsset FindAssetWithFilename(string filename) => Assets.FirstOrDefault(a => a.Name == filename);
        }

        private class GithubApiAsset
        {
            public string Name { get; set; }
            
            [JsonPropertyName("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }

            public ReleaseData.AssetData AsAssetData() => new ReleaseData.AssetData
            {
                Download = BrowserDownloadUrl,
                SHA512 = BrowserDownloadUrl[..BrowserDownloadUrl.LastIndexOf('.')] + ".sha512"
            };
        }
    }
}