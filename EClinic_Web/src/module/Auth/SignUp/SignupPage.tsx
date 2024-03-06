import Head from "next/head"
import FormSignup from "./components/Form"

const SignupPage = () => {
  return (
    <>
      <Head>
        <title>Sign Up</title>
      </Head>
      <div className="mx-6">
        <FormSignup />
      </div>
    </>
  )
}

export default SignupPage
