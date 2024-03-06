/* eslint-disable react/display-name */
import { memo, useCallback, useMemo, useRef } from "react"

import "react-quill/dist/quill.snow.css"

import { ImageActions } from "@xeger/quill-image-actions"
import { ImageFormats } from "@xeger/quill-image-formats"
import ReactQuill, { Quill } from "react-quill"
import { blogService } from "services/blog.service"
import { toast } from "react-hot-toast"
import { isImage } from "shared/helpers/helper"

Quill.register("modules/imageActions", ImageActions)
Quill.register("modules/imageFormats", ImageFormats)

const Editor = memo(
  ({
    value,
    onChange,
    isError = false,
    placeholder
  }: {
    value: string
    onChange: (value: string) => void
    isError?: boolean
    placeholder?: string
  }) => {
    const editorRef: any = useRef()

    const imageHandler = useCallback(() => {
      const input: any = document.createElement("input")
      input.setAttribute("type", "file")
      input.setAttribute("accept", "image/*")
      input.click()

      input.onchange = async () => {
        const file = input.files[0]

        // file type is only image.
        if (isImage(file)) {
          try {
            const res = await blogService.upLoadImage(file)
            if (res?.isSuccess) {
              insertToEditor(res?.message!)
            } else throw new Error()
          } catch (error) {
            toast.error("You could only upload images")
            console.log("input.onchange= ~ error:", error)
          }
        } else {
          console.warn("You could only upload images.")
        }
      }
    }, [])

    const Editor = useMemo(() => {
      return {
        modules: {
          toolbar: {
            container: [
              [{ size: [] }],
              ["bold", "italic", "underline", "strike", "blockquote"],
              [
                { list: "ordered" },
                { list: "bullet" },
                { indent: "-1" },
                { indent: "+1" }
              ],
              ["link"],
              ["clean"],
              ["image"],
              [
                { align: "" },
                { align: "center" },
                { align: "right" },
                { align: "justify" }
              ]
            ],

            handlers: {
              image: imageHandler
            }
          },
          clipboard: {
            // toggle to add extra line breaks when pasting HTML:
            matchVisual: false
          },
          imageActions: {},
          imageFormats: {}
        },
        formats: [
          "header",
          "size",
          "font",
          "bold",
          "italic",
          "underline",
          "strike",
          "blockquote",
          "list",
          "bullet",
          "indent",
          "link",
          "image",
          "video",
          "align",
          "float",
          "alt",
          "height",
          "width",
          "style"
        ]
      }
    }, [imageHandler])

    function insertToEditor(url: string) {
      const quillObj = editorRef?.current?.getEditor()
      const range = quillObj?.getSelection()
      quillObj.insertEmbed(range.index, "image", url)
    }

    const onChangeValue = (values: string) => {
      const val = values === "<p><br></p>" ? "" : values
      onChange(val)
    }

    return (
      <ReactQuill
        placeholder={placeholder}
        modules={Editor.modules}
        formats={Editor.formats}
        className={isError ? "!border-red-500" : ""}
        theme="snow"
        value={value}
        onChange={onChangeValue}
        ref={editorRef}
      />
    )
  }
)

export default Editor
