import { LANGUAGE } from "shared/constant/constant"
import i18n from "i18next"
import { initReactI18next } from "react-i18next"
import HOME_EN from "locales/en/home.json"
import FORUM_VI from "locales/vi/forum.json"
import FORUM_EN from "locales/en/forum.json"
import HOME_VI from "locales/vi/home.json"
import BASE_VI from "locales/vi/base.json"
import BASE_EN from "locales/en/base.json"
import SER_VI from "locales/vi/services.json"
import SER_EN from "locales/en/services.json"
export const resources = {
  [LANGUAGE.ENGLISH]: {
    ser: SER_EN,
    base: BASE_EN,
    home: HOME_EN,
    forum: FORUM_EN
  },
  [LANGUAGE.VIETNAM]: {
    ser: SER_VI,
    base: BASE_VI,
    home: HOME_VI,
    forum: FORUM_VI
  }
}
export const defaultNS = "home"
i18n.use(initReactI18next).init({
  resources,
  lng: LANGUAGE.ENGLISH,
  ns: ["home", "forum", "base", "ser"],
  defaultNS,
  fallbackLng: LANGUAGE.ENGLISH,
  interpolation: {
    escapeValue: false
  }
})
