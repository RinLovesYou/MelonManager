using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MelonLoader.Interfaces
{
    internal class GitHub
    {
        private static readonly HttpClient Client = new HttpClient();

        static GitHub()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "MelonLauncher (https://melonwiki.xyz)");
        }
        
        internal List<ReleaseData> ReleasesTbl = new List<ReleaseData>();
        private string API_URL = null;
        internal GitHub(string url) => API_URL = url;

        internal void Refresh(bool checkForInstaller = false)
        {
            ReleasesTbl.Clear();
            
            List<GithubApiRelease> githubApiReleases = Client.GetFromJsonAsync<List<GithubApiRelease>>(API_URL).Result;
            githubApiReleases ??= new List<GithubApiRelease>();
            
            foreach (GithubApiRelease release in githubApiReleases)
            {
                List<GithubApiAsset> githubApiAssets = release.Assets;
                string releaseVersion = release.TagName;
                
                bool hasWinX86 = (!releaseVersion.StartsWith("v0.2") && !releaseVersion.StartsWith("v0.1"));
                
                ReleaseData releaseData = new ReleaseData()
                {
                    Version = releaseVersion,
                    IsPreRelease = release.Prerelease,
                };
                
                if (!checkForInstaller)
                {
                    releaseData.Windows_x64 = release.FindAssetWithFilename("MelonLoader.x64.zip").AsAssetData();
                    if (hasWinX86)
                        releaseData.Windows_x86 = release.FindAssetWithFilename("MelonLoader.x86.zip").AsAssetData();
                }
                else
                    releaseData.Installer = release.FindAssetWithFilename("MelonLoader.Installer.exe").AsAssetData();

                ReleasesTbl.Add(releaseData);
            }
            
            ReleasesTbl = ReleasesTbl.OrderBy(x => x.Version).ToList();
            ReleasesTbl.Reverse();
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
            internal AssetData Installer;
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