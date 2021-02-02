import App from 'next/app'
import React from 'react'
import '../styles/globals.css'
import apiService from '../services/apiService'
  
class MyApp extends App {

  static async getInitialProps({ Component, ctx }) {

    let pageProps = {};

    const currentUser = await apiService(ctx).GetCurrentUser();

    if (Component.getInitialProps) {
      ctx.currentUser = currentUser;
      pageProps = await Component.getInitialProps(ctx)
    }
    
    pageProps.currentUser = currentUser;

    return { pageProps };
  }

  render() {
    const { Component, pageProps } = this.props
    return (
      <>
        <Component {...pageProps} />
      </>
    )
  }
}

export default MyApp;