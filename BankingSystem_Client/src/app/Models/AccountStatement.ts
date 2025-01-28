import { TransactionModel } from "./TransactionModel";
import { TransactionRecords } from "./TransactionRecords";




export interface AccountStatement {
  accountNumber: string;
  holderName: string;
  accountType: string;
  balance: number;
  transactionRecords: TransactionRecords;
  transactions: TransactionModel[];
}
