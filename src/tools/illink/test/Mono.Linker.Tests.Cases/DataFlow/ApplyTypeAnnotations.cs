// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.DataFlow
{
	[ExpectedNoWarnings]
	public class ApplyTypeAnnotations
	{
		public static void Main ()
		{
			TestFromTypeOf ();
			TestFromTypeGetTypeOverConstant ();
			TestFromStringContantWithAnnotation ();
			TestFromStringConstantWithGeneric ();
			TestFromStringConstantWithGenericAndAssemblyQualified ();
			TestFromStringConstantWithGenericAndAssemblyQualifiedInvalidAssembly ();
			TestFromStringConstantWithGenericAndAssemblyQualifiedNonExistingAssembly ();
		}

		[Kept]
		static void TestFromTypeOf ()
		{
			RequireCombination (typeof (FromTypeOfTestType));
		}

		[Kept]
		class FromTypeOfTestType
		{
			[Kept]
			public FromTypeOfTestType () { }
			public FromTypeOfTestType (int i) { }

			[Kept]
			public void PublicMethod () { }
			private void PrivateMethod () { }

			[Kept]
			public bool _publicField;
			private bool _privateField;

			[Kept]
			[KeptBackingField]
			public bool PublicProperty { [Kept] get; [Kept] set; }
			private bool PrivateProperty { get; set; }
		}

		[Kept]
		static void TestFromTypeGetTypeOverConstant ()
		{
			RequireCombination (Type.GetType ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromTypeGetTypeOverConstantTestType"));
		}

		[Kept]
		class FromTypeGetTypeOverConstantTestType
		{
			[Kept]
			public FromTypeGetTypeOverConstantTestType () { }
			public FromTypeGetTypeOverConstantTestType (int i) { }

			[Kept]
			public void PublicMethod () { }
			private void PrivateMethod () { }

			[Kept]
			public bool _publicField;
			private bool _privateField;

			[Kept]
			[KeptBackingField]
			public bool PublicProperty { [Kept] get; [Kept] set; }
			private bool PrivateProperty { get; set; }
		}

		[Kept]
		static void TestFromStringContantWithAnnotation ()
		{
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithAnnotationTestType");
		}

		[Kept]
		class FromStringConstantWithAnnotationTestType
		{
			[Kept]
			public FromStringConstantWithAnnotationTestType () { }
			public FromStringConstantWithAnnotationTestType (int i) { }

			[Kept]
			public void PublicMethod () { }
			private void PrivateMethod () { }

			[Kept]
			public bool _publicField;
			private bool _privateField;

			[Kept]
			[KeptBackingField]
			public bool PublicProperty { [Kept] get; [Kept] set; }
			private bool PrivateProperty { get; set; }
		}

		[Kept]
		private static void RequireCombination (
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			[DynamicallyAccessedMembers(
				DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
				DynamicallyAccessedMemberTypes.PublicFields |
				DynamicallyAccessedMemberTypes.PublicMethods |
				DynamicallyAccessedMemberTypes.PublicProperties)]
			Type type)
		{
		}

		[Kept]
		private static void RequireCombinationOnString (
			[KeptAttributeAttribute(typeof(DynamicallyAccessedMembersAttribute))]
			[DynamicallyAccessedMembers(
				DynamicallyAccessedMemberTypes.PublicParameterlessConstructor |
				DynamicallyAccessedMemberTypes.PublicFields |
				DynamicallyAccessedMemberTypes.PublicMethods |
				DynamicallyAccessedMemberTypes.PublicProperties)]
			string typeName)
		{
		}

		// https://github.com/dotnet/runtime/issues/72833
		// NativeAOT doesn't implement full type name parser yet
		[Kept (By = Tool.Trimmer)]
		class FromStringConstantWithGenericInner
		{
		}

		[Kept]
		[KeptMember (".ctor()")]
		class FromStringConstantWithGeneric<T>
		{
			[Kept]
			public T GetValue () { return default (T); }
		}

		// https://github.com/dotnet/runtime/issues/72833
		// NativeAOT doesn't implement full type name parser yet
		[Kept (By = Tool.Trimmer)]
		class FromStringConstantWithGenericInnerInner
		{
			[Kept (By = Tool.Trimmer)]
			public void Method ()
			{
			}

			int unusedField;
		}

		[Kept (By = Tool.Trimmer)]
		class FromStringConstantWithGenericInnerOne<
		[DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicMethods)]
		[KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute), By = Tool.Trimmer)]
		T>
		{
		}

		// https://github.com/dotnet/runtime/issues/72833
		// NativeAOT doesn't implement full type name parser yet
		[Kept (By = Tool.Trimmer)]
		class FromStringConstantWithGenericInnerTwo
		{
			void UnusedMethod ()
			{
			}
		}

		// https://github.com/dotnet/runtime/issues/72833
		// NativeAOT doesn't implement full type name parser yet
		[Kept (By = Tool.Trimmer)]
		class FromStringConstantWitGenericInnerMultiDimArray
		{
		}

		// https://github.com/dotnet/runtime/issues/72833
		// NativeAOT actually preserves this, but for a slightly wrong reason - it completely ignores the array notations
		[Kept]
		[KeptMember (".ctor()", By = Tool.NativeAot)]
		class FromStringConstantWithMultiDimArray
		{
			// https://github.com/dotnet/runtime/issues/72833
			// NativeAOT actually preserves this, but for a slightly wrong reason - it completely ignores the array notations
			[Kept (By = Tool.NativeAot)]
			public void UnusedMethod () { }
		}

		[Kept]
		[KeptMember (".ctor()")]
		class FromStringConstantWithGenericTwoParameters<T, S>
		{
		}

		[Kept]
		static void TestFromStringConstantWithGeneric ()
		{
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGeneric`1[[Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericInner]]");
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericTwoParameters`2[Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericInnerOne`1[Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericInnerInner],Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericInnerTwo]");
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGeneric`1[[Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWitGenericInnerMultiDimArray[,]]]");
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithMultiDimArray[,]");
		}

		[Kept]
		[KeptMember (".ctor()")]
		class FromStringConstantWithGenericAndAssemblyQualified<T>
		{
			[Kept]
			public T GetValue () { return default (T); }
		}

		[Kept]
		static void TestFromStringConstantWithGenericAndAssemblyQualified ()
		{
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+FromStringConstantWithGenericAndAssemblyQualified`1[[Mono.Linker.Tests.Cases.Expectations.Assertions.KeptAttribute,Mono.Linker.Tests.Cases.Expectations]]");
		}

		class InvalidAssemblyNameType
		{
		}

		[Kept]
		static void TestFromStringConstantWithGenericAndAssemblyQualifiedInvalidAssembly ()
		{
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+InvalidAssemblyNameType,Invalid/Assembly/Name");
		}

		class NonExistingAssemblyType
		{
		}

		[Kept]
		static void TestFromStringConstantWithGenericAndAssemblyQualifiedNonExistingAssembly ()
		{
			RequireCombinationOnString ("Mono.Linker.Tests.Cases.DataFlow.ApplyTypeAnnotations+InvalidAssemblyNameType,NonExistingAssembly");
		}
	}
}
