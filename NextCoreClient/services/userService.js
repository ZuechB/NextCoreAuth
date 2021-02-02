import { UserManager, WebStorageStateStore } from 'oidc-client';
import { CookieStateStore } from '../services/cookieStorage';
//import { parseCookies, setCookie, destroyCookie } from "nookies";

const config = {
    authority: "https://localhost:44374",
    client_id: "wewantdoughnuts",
    redirect_uri: "http://localhost:3000/signin-oidc",
    response_type: "id_token token",
    scope: "openid profile email doughnutapi",
    post_logout_redirect_uri: "http://localhost:3000/signout-oidc"
};

export function GetUser(ctx = null)
{
    config.userStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });
    config.stateStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });

    let userManager = new UserManager(config);
    return userManager.getUser();
}

export function signinRedirect(ctx = null) {

  config.userStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });
  config.stateStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });

    let userManager = new UserManager(config);
  return userManager.signinRedirect()
}

export function signinRedirectCallback(ctx = null) {

  config.userStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });
  config.stateStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });

    let userManager = new UserManager(config);
  return userManager.signinRedirectCallback()
}

export function signoutRedirect(ctx = null) {

  config.userStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });
  config.stateStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });

  let userManager = new UserManager(config);
  userManager.clearStaleState()
  userManager.removeUser()
  return userManager.signoutRedirect()
}

export function signoutRedirectCallback(ctx = null) {

  config.userStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });
  config.stateStore = new WebStorageStateStore({ store: new CookieStateStore({ctx: ctx}) });

    let userManager = new UserManager(config);
  userManager.clearStaleState()
  userManager.removeUser()
  
  return userManager.signoutRedirectCallback();
}