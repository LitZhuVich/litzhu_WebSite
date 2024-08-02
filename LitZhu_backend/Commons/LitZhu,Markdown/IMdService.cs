
namespace LitZhu_Markdown
{
    public interface IMdService
    {
        /// <summary>
        /// 将MarkDown转换为HTML
        /// </summary>
        /// <param name="md">MD格式的数据</param>
        /// <returns>HTML格式的数据</returns>
        string MdConvertToHtml(string md);
    }
}