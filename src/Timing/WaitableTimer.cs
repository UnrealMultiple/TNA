using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;

namespace Microsoft.Xna.Framework.Timing;

public partial class WaitableTimer : WaitHandle
{
	[LibraryImport("kernel32.dll", EntryPoint = "CreateWaitableTimerW")]
	private static partial SafeWaitHandle CreateWaitableTimer(IntPtr lpTimerAttributes, [MarshalAs(UnmanagedType.Bool)] bool bManualReset, [MarshalAs(UnmanagedType.LPWStr)] string lpTimerName);

	[LibraryImport("kernel32.dll",  SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool SetWaitableTimer(SafeWaitHandle hTimer, ref long pDueTime, int lPeriod, IntPtr pfnCompletionRoutine, IntPtr lpArgToCompletionRoutine, [MarshalAs(UnmanagedType.Bool)] bool fResume);

	public WaitableTimer(bool manualReset = true, string timerName = null)
	{
		SafeWaitHandle = CreateWaitableTimer(IntPtr.Zero, manualReset, timerName);
	}

	public void Set(long dueTime)
	{
		if (!SetWaitableTimer(SafeWaitHandle, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, false))
		{
			throw new Win32Exception();
		}
	}

	// Negative values indicate relative time.
	// See https://learn.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-setwaitabletimer#parameters
	public void SetRelativeTimeSpan(TimeSpan timeSpan)
	{
		Set(-timeSpan.Ticks);
	}
}
