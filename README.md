# Espera.Network

This repository contains the DTOs and network protocol helpers for the [Espera](https://github.com/flagbug/Espera) remote control API.

## Protocol

The protocol is a simple fixed-header TCP protocol

### Structure

Each message begins with a 4-byte header that describes the length of the message content.

This is followed by a GZip-compressed, JSON-serialized representation of the 
[NetworkMessage](https://github.com/flagbug/Espera.Network/blob/master/Espera.Network/NetworkMessage.cs) class.
