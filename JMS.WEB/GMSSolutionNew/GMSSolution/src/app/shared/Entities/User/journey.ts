import { Level } from './level.enum';
import { JourneyStatus } from './journey-status.enum';

import { JourneyUpdate } from './journey-update';
import { AssessmentQuestion } from './assessment-question';
import { User } from '../Login/user';

export class Journey {
  Id: Number;
  Title:string;
  FromLat: Number;
  FromLng: Number;
  ToLat: Number;
  ToLng: Number;
  IsTruckTransport: boolean;
  JourneyStatus: JourneyStatus;
  FromDestination: string;
  ToDistination: string;
  StartDate: Date;
  DeliveryDate: Date;
  CargoWeight: number;
  CargoPriority: Level;
  CargoSeverity: Level;
  UserId: string;
  CargoType: string;
  IsThirdParty: boolean;
  User: User;
  JourneyUpdate: JourneyUpdate[];
  AssessmentQuestion: AssessmentQuestion[];

}
