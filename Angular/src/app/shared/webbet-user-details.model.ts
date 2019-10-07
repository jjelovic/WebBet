export class WebbetUserDetails {
    userName: string;
    fullName: string;
    userWalletBalance: number;


    constructor(userName, fullName, userWalletBalance) {
        this.userName = userName,
        this.fullName = fullName,
        this.userWalletBalance = userWalletBalance
    }
}
