import Head from "next/head"
import React from "react"
import Form from "./components/formForgotPassword"

const ForgotPasswordPage = () => {
  return (
    <>
      <Head>
        <title>Forgot password</title>
        <meta name="description" content="Generated by create next app" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/images/favicon.png" />
      </Head>
      <div className="mx-6">
        <Form />
      </div>
    </>
  )
}

export default ForgotPasswordPage