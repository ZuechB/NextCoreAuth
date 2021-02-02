import axios from 'axios'
import { GetUser } from '../services/userService'
import { signinRedirect, signoutRedirect } from '../services/userService'

const setupDefaultOptions = async (ctx = null) => {

    let defaultOptions = {};
    let identityUser = await GetUser(ctx);

    if (identityUser !== null && identityUser !== undefined && identityUser.access_token != "") {
        defaultOptions = {
            headers: {
                Authorization: "Bearer " + identityUser.access_token
            }
        };
    }
    else {
        defaultOptions = {
            headers: {
            },
        };
    }

    return defaultOptions;
}

const apiService = (ctx = null) => {

    let baseUri = process.env.APIURI;

    return {

        get: async (url, options= {}) => {
            
            let defaultOptions = await setupDefaultOptions(ctx);
            return await axios.get(baseUri + url, { ...defaultOptions, ...options });
        },
        post: async (url, data, options = {}) => {

            let defaultOptions = await setupDefaultOptions(ctx);
            return await axios.post(baseUri + url, data, { ...defaultOptions, ...options });
        },
        put: async (url, data, options = {}) => {

            let defaultOptions = await setupDefaultOptions(ctx);
            return await axios.put(baseUri + url, data, { ...defaultOptions, ...options });
        },
        delete: async (url, options = {}) => {

            let defaultOptions = await setupDefaultOptions(ctx);
            return await axios.delete(baseUri + url, { ...defaultOptions, ...options });
        },
        login: async () => {

            await signinRedirect(ctx);
        },
        logout: async () => {

            await signoutRedirect(ctx);
        },
        GetCurrentUser: async () => {

            let defaultOptions = await setupDefaultOptions(ctx);
            const response = await axios.get(baseUri + '/User', defaultOptions);
            if (response != null && response.status == 200)
            {
                return response.data;
            }
            return null;
        }
    }
}

export default apiService;