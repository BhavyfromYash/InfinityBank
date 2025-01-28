// customer.model.ts

import { Address } from "./Address";

export interface CustomerCreationModel {
  title: string;
  fname: string;
  mname: string;
  lname: string;
  mobileNo: string;
  emailId: string;
  aadhaarNo: string;
  panCardNo: string;
  dob: Date;
  occupationType: string;
  sourceOfIncome: number;
  grossAnnualIncome: number;
  debitCard: boolean;
  netBanking: boolean;
  status: string;
  address: Address; 
}
