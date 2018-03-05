namespace Bricelam.EntityFrameworkCore.FSharp

open Microsoft.EntityFrameworkCore.Internal

module internal IndentedStringBuilderUtilities =

    let append (text:string) (sb:IndentedStringBuilder) =
        sb.Append(text)

    let appendLine (text:string) (sb:IndentedStringBuilder) =
        sb.AppendLine(text)

    let private prependLine (addLineBreak: bool byref) (text:string) (sb:IndentedStringBuilder) =
        if addLineBreak then
            sb |> appendLine "" |> ignore
        else
            addLineBreak <- true

        sb |> append text |> ignore

    let appendLines (lines: string seq) skipFinalNewLine (sb:IndentedStringBuilder) =

        let mutable addLineBreak = false

        lines |> Seq.iter(fun l -> sb |> prependLine &addLineBreak l)

        if skipFinalNewLine then
            sb
        else
            sb |> appendLine ""

    let appendLineIndent message (sb:IndentedStringBuilder) =
        using (sb.Indent())
            (fun _ -> sb.AppendLine(message))    

    let indent (sb:IndentedStringBuilder) =
        sb.IncrementIndent()

    let unindent (sb:IndentedStringBuilder) =
        sb.DecrementIndent()

    let writeNamespaces namespaces (sb:IndentedStringBuilder) =
        namespaces
            |> Seq.iter(fun n -> sb.AppendLine("open " + n) |> ignore)
        sb

    let appendAutoGeneratedTag (sb:IndentedStringBuilder) =
        sb |> appendLine "// <auto-generated />"
    
