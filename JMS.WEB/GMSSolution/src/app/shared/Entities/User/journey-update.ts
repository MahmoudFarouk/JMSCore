import { Level } from './level.enum';
import { JourneyStatus } from './journey-status.enum';
import { Checkpoint } from './checkpoint';
import { Journey } from './journey';
import { AssessmentResult } from './assessment-result';
import { User } from './user';

export class JourneyUpdate {
  Id: Number;
  Date: Date;
  JourneyId: Number;
  JourneyStatus: JourneyStatus;
  VehicleNo: string;
  DriverId: string;
  IsJourneyCheckpoint: boolean;
  CheckpointId: Number;
  RiskLevel: Level;
  Latitude: Number;
  Longitude: Number;
  IsDriverStatus:boolean;
  IsAlert:boolean;
  StatusMessage:string;
  UserId:string;
  Checkpoint:Checkpoint;
  Journey:Journey;
  AssessmentResult:AssessmentResult[];
  User:User;
}
