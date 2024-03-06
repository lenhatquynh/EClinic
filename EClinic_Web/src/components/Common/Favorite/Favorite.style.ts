import { css, keyframes } from "@emotion/react"
import styled from "@emotion/styled"
const animationUp = keyframes`
  0% {
    transform: scale(1) rotate(0deg);
  }
  50% {
    transform: scale(1.34) rotateY(90deg);
  }
`

const animationDown = keyframes`
  0% {
    transform: scale(1.34) rotateY(90deg);
  }
  50% {
    transform: scale(1) rotateY(0);
  }
`

const AddAnimation = css`
  animation: ${animationUp} 0.4s cubic-bezier(0.38, 0.7, 0.6, 0.29);
`

const RemoveAnimation = css`
  animation: ${animationDown} 0.55s cubic-bezier(0.38, 0.7, 0.6, 0.29);
`

const FavoriteWrapper = styled.button`
  display: flex;
  cursor: pointer;
  border: 0;
  gap: 5px;
  background-color: transparent;
  &:hover,
  &:focus {
    border: 0;
    box-shadow: none;
    outline: none;
  }

  svg {
    width: 22px;
    height: 20px;
    ${RemoveAnimation}
    path {
      fill: rgba(0, 0, 0, 0.25);
      stroke: "#ffffff";
      strokewidth: 2px;
      transition: fill 1s ease;
    }
  }

  &.active {
    svg {
      ${AddAnimation};
      path {
        fill: #fc5c63;
      }
    }
  }
`

export default FavoriteWrapper
