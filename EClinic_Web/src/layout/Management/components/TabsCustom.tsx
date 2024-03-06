import styled from "@emotion/styled"
import { Tab, Tabs } from "@mui/material"
import { ReactNode, useState } from "react"
const TabsStyles = styled(Tabs)`
  .MuiTabs-flexContainer {
    .Mui-selected {
      color: #171822;
      font-weight: 600;
    }
  }
  .css-i32t7c-MuiTabs-indicator {
    background-color: #171822;
  }
`
interface Props {
  tabs: {
    key: number
    label: string
    children: ReactNode
  }[]
  defaultTabIndex?: number
}
const TabsCustom = ({ tabs, defaultTabIndex = 0 }: Props) => {
  const [tabIndex, setTabIndex] = useState(defaultTabIndex)
  return (
    <>
      <TabsStyles value={tabIndex} onChange={(_, value) => setTabIndex(value)}>
        {tabs.map((tab, index) => (
          <Tab
            label={tab.label}
            key={index}
            value={tab.key}
            className="px-0 mr-[18px] text-base font-normal normal-case text-disable"
          />
        ))}
      </TabsStyles>
      {tabs.map((tab, index) => {
        if (tab.key === tabIndex) {
          return <div key={index}>{tab.children}</div>
        }
        return null
      })}
    </>
  )
}

export default TabsCustom
