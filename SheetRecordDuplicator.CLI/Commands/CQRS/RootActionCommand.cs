using System;
using System.CommandLine;

namespace SheetRecordDuplicator.CLI.Commands.CQRS;

public record RootActionCommand(ParseResult ParseResult);