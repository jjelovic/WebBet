export class WebbetUserDetails {
    userName: string;
    fullName: string;
    userWalletBalance: number;
    userId : string;


    constructor(userName, fullName, userWalletBalance, userId) {
        this.userName = userName,
        this.fullName = fullName,
        this.userWalletBalance = userWalletBalance,
        this.userId = userId
    }
}
