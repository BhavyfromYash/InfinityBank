import { Time } from "@angular/common";

export interface LogOut{
    LogOutId : number;
    LastLogin: Time;
    SessionExpired: Time;
    Suggestion: string;
}