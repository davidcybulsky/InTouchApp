import {IEnvoronment} from "./environment.interface";

export class EnvironmentDev implements IEnvoronment {
  serverEndpoint: string = "https://localhost:7211";
}
