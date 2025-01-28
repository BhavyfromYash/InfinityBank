import { CanActivateFn } from '@angular/router';
import { SessionService } from '../services/session.service';

export const userGuard: CanActivateFn = (route, state) => {
  let userstatus = localStorage.getItem("logged")
  var loginToken= sessionStorage.getItem("loginToken");
  console.log(loginToken);
  console.log("userstatus" + userstatus);
  if (loginToken)
    return true;
  else {
    alert("You are not logged")
    return false;
  }
};
