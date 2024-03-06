export const VERSION = 1
export const DEFAULT_URL_API = "https://localhost:8888"

export const QUERY_KEYS = {
  LOGIN: "LOGIN",
  SERVICE_PACKAGE: "SERVICE_PACKAGE",
  SERVICE: "SERVICE",
  SPECIALIZATION: "SPECIALIZATION",
  PROFILE: "PROFILE",
  FORUM: {
    POST: "POST_FORUM",
    COMMENT: "COMMENT_FORUM",
    ANSWER: "ANSWER_FORUM"
  },
  HASHTAG: "HASHTAG",
  BLOG: {
    POST: "POST_BLOG",
    HASHTASH: "POST_BLOG_HASHTAG"
  },
  CHAT: {
    MESSAGE: "CHAT_MESSAGE",
    ROOM: "CHAT_ROOM",
    ROOM_TYPE: "CHAT_ROOM_TYPE",
    NEW_ROOM: "CHAT_NEW_ROOM"
  },
  PAYMENT: "PAYMENT",
  AI: {
    MachineLearning: "MachineLearning",
    DeepLearning: "DeepLearning",
    Model: "Model",
    History: "History",
    AIPredict: "AIPredict"
  },
  BOOKING: {
    BOOKING: "BOOKING/BOOKING",
    SERVICE: "BOOKING/SERVICE",
    DOCTOR: "BOOKING/DOCTOR"
  }
} as const
export const ROLE = {
  USER: "User",
  ADMIN: "Admin",
  DOCTOR: "Doctor",
  SUPPORTER: "Supporter",
  EXPERT: "Expert"
} as const
export const URL_API = {
  AUTH: "identity/Authentication",
  ACCOUNT: "identity/Account",
  MAIL: "mail/Mail",
  SERVICE_PACKAGE: "serviceinformation/ServicePackage",
  SERVICE: "serviceinformation/Service",
  SPECIALIZATION: "serviceinformation/Specialization",
  PROFILE: "profile/Profile",
  PROFILE_OTHER: "profile/Other",
  RELATIONSHIPS: "profile/Relationship",
  FORUM_POST: "forum/Post",
  BLOG_POST: "Blog/Blog",
  BLOG_HASHTAG: "Blog/Hashtag",
  FORUM_POST_COMMENT: "forum/Comment",
  FORUM_POST_ANWERS: "forum/Answer",
  FORUM_POST_HASHTAG: "forum/Hashtag",
  PAYMENT: {
    TRANSACTION: "payment/Transaction",
    MOMO: "payment/MomoPayment",
    VNPAY: "payment/VNPayPayment"
  },
  //Chat
  CHAT: {
    ROOM: "Communication/Rooms",
    CHATMESSAGE: "Communication/ChatMessages",
    RoomTypes: "Communication/RoomTypes"
  },
  AI: {
    MachineLearning: "MachineLearning",
    DeepLearning: "DeepLearning",
    Model: "Model",
    History: "History",
    AIPredict: "AIPredict"
  },
  BOOKING: {
    BOOKING: "booking/Booking",
    DOCTOR: "booking/BookingDoctor",
    SERVICE: "booking/BookingPackage",
    DOCTOR_SCHEDULE: "booking/DoctorSchedule"
  }
} as const
export const DEFAULT_ROUTER = {
  USER: "/user/",
  ADMIN: "/admin/",
  DOCTOR: "/doctor/",
  SUPPORTER: "/sup/chat",
  EXPERT: "/expert/"
}
export const LANGUAGE = {
  VIETNAM: "Vi",
  ENGLISH: "En"
} as const
export const LOCALSTORAGE = {
  LANGUAGE: "lng"
} as const
export const RELATIONSHIPS = {
  PARENTS: "Cha Mẹ",
  SIBLINGS: "Anh Chị",
  COUPLE: "Vợ Chồng",
  GRANDPARENTS: "Ông Bà",
  BROTHER: "Em",
  ME: "Me",
  CHILREN: "Con cái"
}
export const TIME_STYPE_PAYMENT = {
  Day: 0,
  Month: 1,
  Year: 2
}
export const PAGE_SIZE = 10
export const PAYMENT = {
  MOMO: {
    name: "momo",
    image: "/images/momo.png"
  },
  VNPAY: {
    name: "vnpay",
    image: "/images/vnpay.png"
  }
}
export const STATUS_BOOKING = {
  UPCOMING: 1,
  DONE: 2,
  CANCEL: 3
}
export const BOOKING_TYPE = {
  Online: 0,
  Offline: 1
}
export const CALL_STATUS = {
  CAMERA: 1,
  MIC: 2,
  ENDCALL: 3
}
export const RESEND_CODE_TYPE = {
  SIGNUP: 0,
  RESET_PASSWORD: 1
}
export const STATISTIC_STATUS = {
  Increase: 0,
  Decrease: 1,
  Equal: 2
}
