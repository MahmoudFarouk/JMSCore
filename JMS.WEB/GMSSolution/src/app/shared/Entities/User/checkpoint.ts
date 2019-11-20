import { JourneyUpdate } from './journey-update';

export class Checkpoint {
  Id: Number;
  Name: string;
  Latitude: Number;
  Longitude: Number;
  IsThirdParty: boolean;
  JourneyUpdate: JourneyUpdate[];
}
