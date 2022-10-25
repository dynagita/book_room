import Command from './ApiCommand';
import Query from './ApiQuery';

export const post = async data =>{
    var url = "Room";
    return await Command.post(url, data);
};

export const get = async () => {
    var url = "Room"
    return await Query.get(url);
}
