import { keyframes } from "@emotion/react"
import styled from "@emotion/styled"
import React from "react"

const dualRing = keyframes`
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
`

const Loader = styled.div`
  &:after {
    content: " ";
    display: block;
    width: 28px;
    height: 28px;
    border-radius: 50%;
    border: 4px solid ${(props) => (props?.color ? props.color : " #fff")};
    border-color: ${(props) => (props?.color ? props.color : " #fff")}
      transparent ${(props) => (props?.color ? props.color : " #fff")}
      transparent;
    animation: ${dualRing} 1.2s linear infinite;
  }
`

export const Spinner = ({ color = "#fff" }) => {
  return <Loader color={color} />
}
export const LoadingArea = () => {
  return (
    <>
      <div className="absolute bottom-0 left-0 z-20 flex items-center justify-center w-full h-full ">
        <Loader color="#024ED5" />
      </div>
    </>
  )
}

export default Spinner
