using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud.TagCloudService
{
    public interface ITagCloudService
    {
        IEnumerable<WordTag> GetWordTags(IEnumerable<string> wordsData, IEnumerable<string> boringWords);
    }
}
