bl.messages = {};

bl.messages.conversation_messages = async (rent_id) => {

    var url = '/api/messages/conversation_messages/' + rent_id;
    return await ajax.get(url);

}  