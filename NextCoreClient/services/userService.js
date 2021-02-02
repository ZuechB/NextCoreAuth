import { UserManager, WebStorageStateStore } from 'oidc-client';
import { CookieStateStore } from '../services/cookieStorage';

const config = {
    authority: process.env.AUTHORITYURI,
    client_id: process.env.client_id,
    redirect_uri: process.env.WebsiteBaseUri + "/signin-oidc",
    response_type: "id_token token",
    scope: "openid profile email " + process.env.Scope,
    post_logout_redirect_uri: process.env.WebsiteBaseUri + "/signout-oidc"
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