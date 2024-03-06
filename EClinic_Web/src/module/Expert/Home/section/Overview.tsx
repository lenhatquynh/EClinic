import React from "react"
import ListMachineLearning from "./components/ListMachineLearning"
import ListDeepLearning from "./components/ListDeepLearning"
import ListModel from "./components/ListModel"

const Overview = () => {
  return (
    <div className="flex flex-col w-full gap-y-6">
      <div className="grid grid-cols-2 gap-6">
        <ListMachineLearning />
        <ListDeepLearning />
      </div>
      <ListModel />
    </div>
  )
}

export default Overview
