using System.Drawing;
using TagCloud.Models;

namespace TagCloud.CloudDrawer
{
    public interface ICloudDrawer
    {
        void DrawWordsAndSave(IEnumerable<WordTag> words);
    }
}
