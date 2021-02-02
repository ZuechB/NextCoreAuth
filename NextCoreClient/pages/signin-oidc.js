import React, { useEffect } from 'react'
import { signinRedirectCallback } from '../services/userService'
import { useRouter } from 'next/router'

function SigninOidc() {
  const router = useRouter();

  useEffect(() => {
    async function signinAsync() {
      await signinRedirectCallback();
      router.push('/');
    }
    signinAsync()
  })

  return (
    <div>
      Redirecting...
    </div>
  )
}

export default SigninOidc
