using Markdig;
using Markdig.SyntaxHighlighting;

namespace LitZhu_Markdown;

public class MdService : IMdService
{
    public string MdConvertToHtml(string md)
    {
        // 创建 MarkdownPipelineBuilder 实例并配置代码高亮
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseSyntaxHighlighting()
            // 添加样式配置
            .UseYamlFrontMatter()
            .UseFootnotes()
            .UseEmojiAndSmiley()
            .UseMediaLinks()
            .UsePipeTables()
            .UseGridTables()
            .UseSmartyPants()
            .UseSoftlineBreakAsHardlineBreak()
            .UseAutoLinks()
            .UseAutoIdentifiers()
            .UseAbbreviations()
            .Build();

        // 将 Markdown 转换为 HTML
        string html = Markdown.ToHtml(md, pipeline);

        // 返回 HTML
        return html;
    }
}