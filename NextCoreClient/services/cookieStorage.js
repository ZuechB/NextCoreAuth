import React from "react";
import { parseCookies, setCookie, destroyCookie } from "nookies";

export class CookieStateStore extends React.Component {

  constructor(props) {
    super(props);
  }

  setItem = (key, value) => {
    
    setCookie(this.props.ctx, this.getKey(key), value, {
    });
    return Promise.resolve();
  }

  getItem = (key) => {
    const value = parseCookies(this.props.ctx)[this.getKey(key)];

    if (value) {
      return Promise.resolve(value);
    } else {
      return Promise.resolve(value);
    }
  }

  removeItem = (key) => {
    const value = parseCookies(this.props.ctx)[this.getKey(key)];
    destroyCookie(this.props.ctx, this.getKey(key), {});

    if (value) {
      return Promise.resolve(value);
    } else {
      return Promise.resolve(value);
    }
  }

  getAllKeys = () => {
    const keys = [];
    return Promise.resolve(keys);
  }

  getKey = (key) => {
    return `${key}`;
  }

}