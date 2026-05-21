class CommentBox extends React.Component {
    users = this.props.initialData;
    selectedUser = this.users[0];
    handleUserSwitch = event => 
    {
        this.selectedUser= this.users.find(({ id }) => id === event.target.selectedIndex);
        this.setState({ selectedUser: this.users.find(({ id }) => id === event.target.selectedIndex) });
    }
    handleDateChange = date =>
    {
        this.chosenDate= date;
        this.setState({ chosenDate: date });
    }
    
    handleCommentSubmit = type => {
        var data = JSON.stringify({
            "personId": this.selectedUser.id,
            "bookingType": type,
            "time": "2026-05-21T14:17:53.385Z"
        });

        var xhr = new XMLHttpRequest();

        xhr.open("POST", this.props.submitUrl);
        xhr.setRequestHeader("Content-Type", "application/json");

        xhr.send(data);
    };
    
    render() {
        return (
            <div className="mainBooking">
                <h1>Бронирование</h1>
                
                <UserInfo handleUserSwitch={x => this.handleUserSwitch(x)} users={this.users} handleDateChange={x => this.handleDateChange(x)}/>
                <UserForm user={this.selectedUser} />
                <SendButtons submitHandler={x => this.handleCommentSubmit(x)} />
               
            </div>
        );
    }
}
// <CommentForm onCommentSubmit={this.handleCommentSubmit} />

class UserInfo extends React.Component {
    date = new Date();
    
    render() {
        const users = this.props.users.map(user => (
            <option key={user.id}>{user.name}({user.nameCode})</option>
        ));

        return <div className="userInfo">
            <div><label className="form-label">На кого бронируем</label></div>
            <select onChange={this.props.handleUserSwitch}>
                {users}
            </select>
            <div><label className="form-label">Когда бронируем</label></div>
            <label className="form-label">понедельник с 12:30 до 16:00, вторник с 14:00 до 16:00, среда с 8:00 до 12:00. </label>
            <div><input type="datetime-local" value={this.date} onChange={x => this.props.handleDateChange(x)}/></div>
            

        </div>;
    }
}

class UserForm extends React.Component {
    render() {
        return <div className="userForm">
        <div><b>Организация:</b> {this.props.user.orgName}</div>
                    <div><b>Спецучет:</b> {this.props.user.orgCode}</div>
                    <div><b>Именник:</b> {this.props.user.nameCode}</div>
                    <div><b>ФИО:</b> {this.props.user.name}</div>
                    <div><b>Email:</b> {this.props.user.email}</div>
                    <div><b>Телефон:</b> {this.props.user.phone}</div>
               </div>;
    }
}

class SendButtons extends React.Component {
    render() {
        return <div className="sendButtons">
                    <button onClick={_ => this.props.submitHandler(108)}>Забронировать срочный</button>
                    <button onClick={_ => this.props.submitHandler(105)}>Забронировать несрочный, до 100</button>
               </div>;
    }
}