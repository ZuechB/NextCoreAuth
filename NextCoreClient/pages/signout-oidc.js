import React, { useEffect } from 'react'
import { signoutRedirectCallback } from '../services/userService'
import { useRouter } from 'next/router'

function SignoutOidc() {
  const router = useRouter();

  useEffect(() => {
    async function signoutAsync() {
      await signoutRedirectCallback()
      router.push('/');
    }
    signoutAsync()
  })

  return (
    <div>
      Redirecting...
    </div>
  )
}

export default SignoutOidc
