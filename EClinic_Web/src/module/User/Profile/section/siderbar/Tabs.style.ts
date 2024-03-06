import styled from "@emotion/styled"
import colorsProvider from "shared/theme/colors"

export const TabsWrapper = styled.div`
  width: 100%;
  display: flex;
  .tab-wrapper {
    .MuiTabs-flexContainer {
      border-radius: 10px;
      display: flex;
      justify-content: flex-start;
      background-color: white;
      padding: 33px 53px 33px 24px;
    }
    .MuiTab-root {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      text-transform: none;
      font-size: 14px;
      padding: 0px 20px;
      min-height: 46px;
      font-weight: 400;
      color: ${colorsProvider.black2};
    }
    .Mui-selected {
      color: ${colorsProvider.primary};
    }
    .MuiTabs-indicator {
      border-radius: 10px;
      background-color: rgba(2, 78, 213, 0.1);
      color: ${colorsProvider.primary};
      width: 80%;
      margin: 0 auto;
      right: 30px;
    }
    @media screen and (max-width: 767.98px) {
      position: fixed;
      left: 0;
      transition: all 0.2s linear;
      z-index: 10;
      box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
      .MuiTabs-flexContainer {
        padding: 24px 42px 24px 24px;
      }
    }
  }
`
