import styled from "@emotion/styled"
import React from "react"

interface Props
  extends React.DetailedHTMLProps<
    React.ButtonHTMLAttributes<HTMLButtonElement>,
    HTMLButtonElement
  > {
  icon?: React.ReactNode
  content: string
  color?: string
}

const StyledButton = styled.button`
  display: inline-flex;
  align-items: center;
  padding: 8px;
  font-size: 14px;
  font-weight: 500;
  text-align: center;
  text-decoration: none;
  border: none;
  border-radius: 4px;
  outline: none;
  cursor: pointer;
  background-color: ${(props) => props.color};
  color: #fff;
  transition: background-color 0.3s ease-in-out;

  &:hover {
    background-color: ${(props) => props.color && `${props.color}90`};
  }

  &:active {
    transform: scale(0.95);
  }

  svg {
    width: 18px;
    height: 18px;
    margin-right: 4px;
  }
`

const ButtonTable = ({
  icon = (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      strokeWidth={2}
      stroke="currentColor"
      className="w-6 h-6"
    >
      <path
        strokeLinecap="round"
        strokeLinejoin="round"
        d="M12 4.5v15m7.5-7.5h-15"
      />
    </svg>
  ),
  content,
  color = "#4FD8DE",
  ...props
}: Props) => {
  return (
    <StyledButton color={color} {...props}>
      {icon}
      <span>{content}</span>
    </StyledButton>
  )
}

export default ButtonTable
