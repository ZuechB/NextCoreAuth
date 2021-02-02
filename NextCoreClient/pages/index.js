import Head from 'next/head'
import styles from '../styles/Home.module.css'
import apiService from '../services/apiService'

export default function Home({currentUser}) {

  const GetUserInfo = async () => {
    let user = await apiService().get("/user");
    if (user != null && user.status == 200)
    {
      alert(user.data.firstName + " " + user.data.lastName);
    }
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
            <span>Welcome home {currentUser.firstName}!</span>
            <br/>
            <button onClick={async () => await apiService().logout()}>Signout</button>

            <button onClick={() => GetUserInfo()}>Request user information from database</button>
            </>
          }

          {currentUser == null &&
            <>
            <span>Login to get started or <br/>create an account</span>
            <br/>
            <button onClick={async () => await apiService().login()}>Login</button>
            <button onClick={() => LoginToApp()}>Create Account</button>
            </>
          }

        </h1>

      </main>

    </div>
  )
}
