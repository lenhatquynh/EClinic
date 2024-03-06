import React from "react"
import Field from "../components/Field"

const About = ({ data = "" }) => {
  return (
    <div className="flex flex-col w-full space-y-5">
      <div
        className="entry-content"
        // Prevent XSS Attack recommen from React Docs
        dangerouslySetInnerHTML={{
          __html: data || ""
        }}
      ></div>
      {/* <Field title="Chuyên môn">
        <p className="text-base font-light leading-relaxed text-gray-500">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptas quis
          obcaecati dicta praesentium officia, ea exercitationem culpa possimus
          nihil aspernatur eligendi veniam vero fugit sunt? Aspernatur delectus
          impedit quidem porro! Lorem ipsum dolor sit amet consectetur
          adipisicing elit. Atque eveniet dignissimos, nostrum doloribus ipsa
          corporis, magni non consequuntur optio totam fugit sint impedit, natus
          perferendis doloremque hic. Officia, iste beatae?
        </p>
      </Field>
      <Field title="Kinh nghiệm làm việc">
        <ul className="space-y-3">
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Build prototype and do usability testing to solve user problems.{" "}
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Follow design system guidelines.{" "}
            </p>
          </li>
        </ul>
      </Field>
      <Field title="Kỹ năng">
        <ul className="space-y-3">
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Build prototype and do usability testing to solve user problems.{" "}
            </p>
          </li>
        </ul>
      </Field>
      <Field title="Học tập">
        <ul className="space-y-3">
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Build prototype and do usability testing to solve user problems.{" "}
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Build prototype and do usability testing to solve user problems.{" "}
            </p>
          </li>
        </ul>
      </Field>
      <Field title="Thành tích">
        <ul className="space-y-3">
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Build prototype and do usability testing to solve user problems.{" "}
            </p>
          </li>
          <li className="flex items-center space-x-2">
            <svg
              width={13}
              height={12}
              viewBox="0 0 13 12"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fillRule="evenodd"
                clipRule="evenodd"
                d="M6.5 11C9.26142 11 11.5 8.76142 11.5 6C11.5 3.23858 9.26142 1 6.5 1C3.73858 1 1.5 3.23858 1.5 6C1.5 8.76142 3.73858 11 6.5 11Z"
                stroke="#50B5FF"
                strokeWidth={2}
              />
            </svg>
            <p className="text-base font-light leading-relaxed text-gray-500">
              Lorem ipsum dolor sit amet consectetur adipisicing elit.
            </p>
          </li>
        </ul>
      </Field> */}
    </div>
  )
}

export default About
