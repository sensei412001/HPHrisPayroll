export interface User {
    userName: string;
    employeeNo: string;
    fullName: string;
    userGroupId: string;
    isEnable: boolean;
    passwordExpiration: Date;
    lastActive: Date;
    dateCreated: Date;
}
