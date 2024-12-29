import { CanActivateFn } from '@angular/router';

export const userGuard: CanActivateFn = (route, state) => {
  let userstatus = localStorage.getItem("logged")
  console.log("userstatus" + userstatus)
  if (userstatus == 'true')
    return true;
  else {
    alert("You are not logged")
    return false;
  }
};
