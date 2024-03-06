import Tag from "components/Common/Tag"
import React, { Component, ErrorInfo, ReactNode } from "react"
import colorsProvider from "shared/theme/colors"

interface ErrorBoundaryProps {
  children: ReactNode
}

interface ErrorBoundaryState {
  hasError: boolean
}

class ErrorBoundary extends Component<ErrorBoundaryProps, ErrorBoundaryState> {
  constructor(props: ErrorBoundaryProps) {
    super(props)

    // Define a state variable to track whether there is an error or not
    this.state = { hasError: false }
  }

  static getDerivedStateFromError(_: Error): ErrorBoundaryState {
    // Update state so the next render will show the fallback UI
    return { hasError: true }
  }

  componentDidCatch(error: Error, errorInfo: ErrorInfo): void {
    // You can use your own error logging service here
    console.log({ error, errorInfo })
  }

  render() {
    // Check if there is an error
    if (this.state.hasError) {
      // You can render any custom fallback UI
      return (
        <div className="flex h-[calc(100vh-80px)] items-center justify-center p-5 w-full bg-white">
          <div className="text-center">
            <div className="inline-flex p-4 bg-red-100 rounded-full">
              <div className="p-4 bg-red-200 rounded-full stroke-red-600">
                <svg
                  className="w-16 h-16"
                  viewBox="0 0 28 28"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M6 8H6.01M6 16H6.01M6 12H18C20.2091 12 22 10.2091 22 8C22 5.79086 20.2091 4 18 4H6C3.79086 4 2 5.79086 2 8C2 10.2091 3.79086 12 6 12ZM6 12C3.79086 12 2 13.7909 2 16C2 18.2091 3.79086 20 6 20H14"
                    strokeWidth={2}
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                  <path
                    d="M17 16L22 21M22 16L17 21"
                    strokeWidth={2}
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  />
                </svg>
              </div>
            </div>
            <h1 className="mt-5 text-[36px] font-bold text-slate-800 lg:text-[50px]">
              Oops something went wrong.
            </h1>
            <p className="mt-5 text-slate-600 lg:text-lg">
              Try to refresh this page or <br /> feel free to contact us if the
              problem presists.
            </p>
            <Tag
              className="px-5 py-3 mx-auto mt-5 text-xl rounded-md cursor-pointer hover:opacity-80"
              color={colorsProvider.primary}
              onClick={() => this.setState({ hasError: false })}
            >
              Try again?
            </Tag>
          </div>
        </div>
      )
    }

    // Return children components in case of no error
    return this.props.children
  }
}

export default ErrorBoundary
