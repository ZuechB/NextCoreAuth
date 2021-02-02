import axios from 'axios'
import { GetUser } from '../services/userService'

async function getSignedInUser(ctx = null) {

    let currentUser = await GetUser(ctx);

    let defaultOptions = {};

    if (currentUser != null)
    {
        defaultOptions = {
            headers: {
                Authorization: "Bearer " + currentUser.access_token
            }
        };

        let options = {};

        const response = await axios.get('http://localhost:55755/api/User', defaultOptions);

        return response.data;
    }
    else
    {
        return null;
    }
}

export {
    getSignedInUser
}
