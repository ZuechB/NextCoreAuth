import Head from 'next/head'
import styles from '../styles/Home.module.css'
import { signinRedirect, signoutRedirect } from '../services/userService'
//import { useEffect, useState } from 'react'
import * as apiService from '../services/apiService'

export default function Home({currentUser}) {

  const LoginToApp = () => {
    signinRedirect();
  }

  const SignOutOfApp = () => {
    signoutRedirect();
  }
  
  const GetUserInfo = async () => {

    const user = await apiService.getSignedInUser();
    alert(user.firstName);
  }

  return (
    <div className={styles.container}>
      <Head>
        <title>Create Next App</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>
        <h1 className={styles.title}>
          {currentUser != null &&
            <>
            <span>Welcome {currentUser.firstName}!</span>
            <br/>
            <button onClick={() => SignOutOfApp()}>Signout</button>


            <button onClick={() => GetUserInfo()}>Request user information from database</button>
            </>
          }

          {currentUser == null &&
            <>
            <span>Login to get started</span>
            <br/>
            <button onClick={() => LoginToApp()}>Login</button>
            </>
          }


        </h1>

      </main>

    </div>
  )
}
