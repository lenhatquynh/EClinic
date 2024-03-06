import { MenuItem, Select, SelectChangeEvent } from "@mui/material"
import classNames from "classnames"
import React from "react"
import { useTranslation } from "react-i18next"
import { LANGUAGE, LOCALSTORAGE } from "shared/constant/constant"

const ChangeLanguage = () => {
  const { i18n } = useTranslation()
  const handleChange = (value: string) => {
    localStorage.setItem(LOCALSTORAGE.LANGUAGE, value)
    i18n.changeLanguage(value)
  }
  return (
    <>
      <ul className="flex items-center space-x-1 text-gray-300 font-extralight">
        <li
          onClick={() => handleChange(LANGUAGE.ENGLISH)}
          className={`cursor-pointer transition-all ${
            i18n.language === LANGUAGE.ENGLISH
              ? "text-primary font-semibold"
              : ""
          }`}
        >
          {LANGUAGE.ENGLISH}
        </li>
        <li>|</li>
        <li
          onClick={() => handleChange(LANGUAGE.VIETNAM)}
          className={`cursor-pointer transition-all ${
            i18n.language === LANGUAGE.VIETNAM
              ? "text-primary font-semibold"
              : ""
          }`}
        >
          {LANGUAGE.VIETNAM}
        </li>
      </ul>
    </>
  )
}

export default ChangeLanguage
